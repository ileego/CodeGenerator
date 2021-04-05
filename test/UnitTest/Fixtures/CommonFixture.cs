using System;
using CodeGenerator.Core;
using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Context;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UnitTest.Fixtures
{
    public class CommonFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public CommonFixture()
        {
            var basePath = ApplicationEnvironment.ApplicationBasePath;

            IServiceCollection services = new ServiceCollection();

            Configuration = new ConfigurationBuilder().AddJsonFile($"{basePath}appsettings.json", false, true).Build();

            //var connectionString = Configuration.GetConnectionString("MyCat");
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

            //注册Infra.Common
            services.AddInfrastructureDependency();
            //注册Core
            services.AddCoreDependency();


            ServiceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
