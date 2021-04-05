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
                    //��ֵָʾ�Ƿ����������������ע�͡�
                    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                    //dynamic�������������л�����
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    //dynamic
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    //��������
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                })
                .AddFluentValidation(options =>
                {
                    //Continue ��֤ʧ�ܣ�������֤������
                    options.ValidatorOptions.CascadeMode = FluentValidation.CascadeMode.Continue;
                    // Optionally set validator factory if you have problems with scope resolve inside validators.
                    // options.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
                    options.RegisterValidatorsFromAssembly(lifetime: ServiceLifetime.Scoped,
                        assembly: typeof(CodeGenerator.Application.Validations.ApiResourceValidator).Assembly);
                });
            //������֤������Ϣ��ʽ����
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //�ر��Զ���֤
                //options.SuppressModelStateInvalidFilter = true;
                //��ʽ����֤��Ϣ
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var problemDetails = new ProblemDetails
                    {
                        Detail = context.ModelState.GetValidationSummary("<br>")
                        ,
                        Title = "��������"
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
            //ע��DbContext Options
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
            //ע��DbContext
            services.AddScoped<DbContext, EfDbContext>();

            //ע�� AutoMapper Profiles
            services.AddAutoMapper(typeof(CodeGenerator.Application.Profiles.MapperProfiles).Assembly);

            //ע��Infra.Common
            services.AddInfrastructureDependency();
            //ע��Core
            services.AddCoreDependency();


            var jwtOptions = ServiceProvider.GetService<IOptions<JwtOptions>>();
            services.AddAuthorization(options =>
            {
                // 1��Definition authorization policy
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
                        //����Ȩ�����Զ�����Ϣ
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
                    Title = "CodeGenerator �ӿ��ĵ� v1.0",
                    Version = "v1.0"
                });
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CodeGenerator.Application.xml"), true);
                //options.OperationFilter<AddAuthTokenHeaderParameter>();
                //Add Jwt Authorize to http header
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Scheme = "bearer",
                    Description = "ʹ��JWT��Ȩ������Header�а���Authorization. ʾ��: \"Authorization: Bearer {token}\"",
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
