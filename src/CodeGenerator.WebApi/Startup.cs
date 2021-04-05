using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CodeGenerator.Core;
using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Context;
using CodeGenerator.Infra.Common.Extensions;
using CodeGenerator.Infra.Common.Filters;
using CodeGenerator.Infra.Common.Helper;
using CodeGenerator.Infra.Common.Options;
using CodeGenerator.Infra.Common.ValueModel;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CodeGenerator.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScopeAssembly("CodeGenerator.Core");
            services.AddAssembly("CodeGenerator.Application", lifetime: ServiceLifetime.Scoped, "", "Service");

            services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
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
                    // Optionally set validator factory if you have problems with scope resolve inside validators.
                    // options.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
                    options.RegisterValidatorsFromAssembly(lifetime: ServiceLifetime.Scoped,
                        assembly: typeof(CodeGenerator.Application.Validations.ApiResourceValidator).Assembly);
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

            ServiceProvider = services.BuildServiceProvider();

            var connectionString = Configuration.GetConnectionString("MySql_Local");
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

            //注入 AutoMapper Profiles
            services.AddAutoMapper(typeof(CodeGenerator.Application.Profiles.MapperProfiles).Assembly);

            //注册Infra.Common
            services.AddInfrastructureDependency();
            //注册Core
            services.AddCoreDependency();


            var jwtOptions = ServiceProvider.GetService<IOptions<JwtOptions>>();
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
                    ValidIssuer = jwtOptions.Value.Issuer,
                    ValidAudience = jwtOptions.Value.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SymmetricSecurityKey)),
                    ClockSkew = TimeSpan.FromMinutes(jwtOptions.Value.AccessTokenExpire),
                    ValidateLifetime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
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
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "CodeGenerator 接口文档 v1.0",
                    Version = "v1.0"
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CodeGenerator.Application.xml"), true);
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseSerilog();

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeGenerator.WebApi");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
