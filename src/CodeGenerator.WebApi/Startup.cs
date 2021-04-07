using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using CodeGenerator.Application;
using CodeGenerator.Core;
using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Extensions;
using CodeGenerator.Infra.Common.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CodeGenerator.WebApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            this.Environment = environment;
            this.Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public IWebHostEnvironment Environment;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //◊¢≤· Controllers
            services.AddControllers(() =>
            {
                return new Assembly[] { typeof(CodeGenerator.Application.Validations.ApiResourceValidator).Assembly };
            });

            //◊¢≤· EfDbContext
            var connectionString = Configuration.GetConnectionString("MySql_Local");
            services.AddEfContext(connectionString);

            //◊¢»Î AutoMapper Profiles
            services.AddAutoMapper(typeof(CodeGenerator.Application.Profiles.MapperProfiles).Assembly);

            //◊¢≤·Infra.Common
            services.AddInfrastructureDependency();

            //◊¢≤·Core
            services.AddCoreDependency();

            //◊¢≤·Application
            services.AddApplicationDependency();

            //Redis≈‰÷√œÓ
            services.Configure<RedisOptions>(Configuration.GetRedisConfigSection());

            //Jwt≈‰÷√œÓ
            services.Configure<JwtOptions>(Configuration.GetJwtConfigSection());
            var jwtOptions = Configuration.GetJwtOptions();

            services.AddJwtAuthentication(jwtOptions);
            services.AddSwaggerGen(
                version: "v1"
                ,
                apiInfo: new OpenApiInfo()
                {
                    Title = "CodeGenerator Ω”ø⁄Œƒµµ v1.0",
                    Version = "v1.0"
                }
                ,
                xmlDocumentPaths: new[]
                {
                    Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"),
                    Path.Combine(AppContext.BaseDirectory, "CodeGenerator.Application.xml")
                });

            //Cors≈‰÷√œÓ
            services.Configure<CorsOptions>(Configuration.GetCorsConfigSection());
            var corsOptions = Configuration.GetCorsOptions();
            services.AddDefaultCors(corsOptions);

            //Authorization≈‰÷√œÓ
            services.Configure<AuthorizationOptions>(Configuration.GetAuthorizationConfigSection());
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
