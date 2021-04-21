using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Context;
using CodeGenerator.Infra.Common.Filters;
using CodeGenerator.Infra.Common.Helper;
using CodeGenerator.Infra.Common.Options;
using CodeGenerator.Infra.Common.ValueModel;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CodeGenerator.Infra.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注册Controllers
        /// System.Text.Json 配置
        /// FluentValidation 注册
        /// ApiBehaviorOptions 配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="fluentAssemblies"></param>
        /// <returns></returns>
        public static IMvcBuilder AddControllers(this IServiceCollection services, Func<IEnumerable<Assembly>> fluentAssemblies)
        {
            var mvcBuilder = services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                    options.JsonSerializerOptions.Converters.Add(new LongConverter());
                    options.JsonSerializerOptions.Converters.Add(new LongNullableConverter());

                    options.JsonSerializerOptions.Encoder = SystemTextJsonHelper.GetDefaultEncoder();
                    //该值指示是否允许、不允许或跳过注释。
                    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                    //dynamic与匿名类型序列化设置
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    //dynamic
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    //匿名类型
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                })
                .AddFluentValidation(options =>
                {
                    //Continue 验证失败，继续验证其他项
                    options.ValidatorOptions.CascadeMode = FluentValidation.CascadeMode.Continue;

                    if (null != fluentAssemblies)
                    {
                        var assemblies = fluentAssemblies.Invoke();
                        var enumerable = assemblies.ToList();
                        if (enumerable.ToList().Any())
                        {
                            options.RegisterValidatorsFromAssemblies(lifetime: ServiceLifetime.Scoped, assemblies: enumerable);
                        }
                    }
                });

            //参数验证返回信息格式调整
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //关闭自动验证
                //options.SuppressModelStateInvalidFilter = true;
                //格式化验证信息
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var problemDetails = new ProblemDetails
                    {
                        Detail = context.ModelState.GetValidationSummary("<br>")
                        ,
                        Title = "参数错误"
                        ,
                        Status = (int)HttpStatusCode.BadRequest
                        ,
                        Type = "https://httpstatuses.com/400"
                        ,
                        Instance = context.HttpContext.Request.Path
                    };

                    return new ObjectResult(problemDetails)
                    {
                        StatusCode = problemDetails.Status
                    };
                };
            });

            return mvcBuilder;
        }

        /// <summary>
        /// 注册 EfDbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void AddEfContext(this IServiceCollection services, string connectionString)
        {
            //注册DbContext Options
            services.AddScoped<DbContextOptions>(c =>
            {
                return new DbContextOptionsBuilder<EfDbContext>()
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                    .UseMySQL(connectionString, options =>
                    {
                        options.CommandTimeout(10000);
                    }
                    ).Options;
            });
            //注册DbContext
            services.AddScoped<DbContext, EfDbContext>();
        }

        /// <summary>
        /// 注册Jwt认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtOptions"></param>
        public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthorization(options =>
            {
                // 1、Definition authorization policy
                options.AddPolicy("Permission",
                    policy => policy.Requirements.Add(new PermissionRequirement()));
            }).AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SymmetricSecurityKey)),
                    ClockSkew = TimeSpan.FromMinutes(jwtOptions.ClockSkew),
                };
                options.Events = new JwtBearerEvents
                {
                    //接受到消息时调用
                    OnMessageReceived = context => Task.CompletedTask
                    ,
                    //在Token验证通过后调用
                    OnTokenValidated = context =>
                    {
                        var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
                        var claims = context.Principal.Claims;
                        if (null == userContext)
                        {
                            userContext = new UserContext();
                            userContext.User ??= new UserModel();
                        }
                        userContext.User.UserId = long.Parse(claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                        return Task.CompletedTask;
                    }
                    ,
                    //认证失败时调用
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                    ,
                    //未授权时调用
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        //无授权返回自定义信息
                        context.Response.WriteAsync(
                            JsonSerializer.Serialize("")
                            );
                        return Task.CompletedTask;

                    }
                };
                //走JWT映射声明，需要将默认映射方式给移除掉
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            });
        }

        /// <summary>
        /// 注册Swagger组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="version"></param>
        /// <param name="apiInfo"></param>
        /// <param name="xmlDocumentPaths"></param>
        public static void AddSwaggerGen(this IServiceCollection services, string version, OpenApiInfo apiInfo, IEnumerable<string> xmlDocumentPaths)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, apiInfo);
                // 为 Swagger JSON and UI设置xml文档注释路径
                foreach (var xmlDocumentPath in xmlDocumentPaths)
                {
                    options.IncludeXmlComments(xmlDocumentPath, true);
                }
                //options.OperationFilter<AddAuthTokenHeaderParameter>();
                //Add Jwt Authorize to http header
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Scheme = "bearer",
                    Description = "使用JWT授权，请在Header中包含Authorization. 示例: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//Jwt default param name
                    In = ParameterLocation.Header,//Jwt store address
                    Type = SecuritySchemeType.ApiKey //Security scheme type
                });
                //Add authentication type

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// 配置 Cors hosts
        /// </summary>
        /// <param name="services"></param>
        /// <param name="corsOptions"></param>
        public static void AddDefaultCors(this IServiceCollection services, CorsOptions corsOptions)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(corsOptions.Hosts)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        /// <summary>
        /// 从Assembly注册AutoMapper配置 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddAutoMapperAssembly(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);
        }
    }
}
