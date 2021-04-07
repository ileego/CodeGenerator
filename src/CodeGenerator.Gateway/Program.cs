using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Extensions;
using CodeGenerator.Infra.Common.Options;

namespace CodeGenerator.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, cb) =>
                {
                    //����������consul�������Ķ�ȡ����
                    var env = context.HostingEnvironment;
                    if (env.IsProduction() || env.IsStaging())
                    {
                        var configuration = cb.Build();
                        var consulOption = configuration.GetSection("Consul").Get<ConsulOptions>();
                        cb.AddConsulConfiguration(new[] { consulOption.ConsulUrl }, consulOption.ConsulKeyPath);
                    }
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile($"{AppContext.BaseDirectory}/Config/ocelot.{env.EnvironmentName}.json", false, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
