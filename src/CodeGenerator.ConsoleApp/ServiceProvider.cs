using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core;
using CodeGenerator.Infra.Common;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeGenerator.ConsoleApp
{
    public class ServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 应用路径
        /// </summary>
        public string ApplicationPath { get; private set; }

        public ServiceProvider()
        {
            ApplicationPath = ApplicationEnvironment.ApplicationBasePath;

            IServiceCollection services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"{ApplicationPath}appsettings.json", false, true).Build();

            //var connectionString = Configuration.GetConnectionString("MyCat");
            var connectionString = configuration.GetConnectionString("MySql_Local");

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

            //注册Infra.Common
            services.AddInfrastructureDependency();
            //注册Core
            services.AddCoreDependency();


            _serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
