[33mcommit 510626b6175e807146485251c3292652d7510383[m[33m ([m[1;36mHEAD -> [m[1;32mmain[m[33m, [m[1;31morigin/main[m[33m, [m[1;31morigin/HEAD[m[33m)[m
Author: È´òÈ£û <fei.gao8@gmail.com>
Date:   Fri Mar 19 17:34:02 2021 +0800

    Â¢ûÂä†Ocelot„ÄÅConsulÔºõÂ¢ûÂä†CAPÊé•Âè£

[1mdiff --git a/CodeGenerator.Gateway/CodeGenerator.Gateway.csproj b/CodeGenerator.Gateway/CodeGenerator.Gateway.csproj[m
[1mnew file mode 100644[m
[1mindex 0000000..83b11b9[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/CodeGenerator.Gateway.csproj[m
[36m@@ -0,0 +1,16 @@[m
[32m+[m[32m<Project Sdk="Microsoft.NET.Sdk.Web">[m
[32m+[m
[32m+[m[32m  <PropertyGroup>[m
[32m+[m[32m    <TargetFramework>netcoreapp3.1</TargetFramework>[m
[32m+[m[32m  </PropertyGroup>[m
[32m+[m
[32m+[m[32m  <ItemGroup>[m
[32m+[m[32m    <PackageReference Include="Ocelot" Version="15.0.7" />[m
[32m+[m[32m    <PackageReference Include="Ocelot.Provider.Consul" Version="15.0.7" />[m
[32m+[m[32m  </ItemGroup>[m
[32m+[m
[32m+[m[32m  <ItemGroup>[m
[32m+[m[32m    <ProjectReference Include="..\src\CodeGenerator.Infra.Common\CodeGenerator.Infra.Common.csproj" />[m
[32m+[m[32m  </ItemGroup>[m
[32m+[m
[32m+[m[32m</Project>[m
[1mdiff --git a/CodeGenerator.Gateway/Config/ocelot.development.json b/CodeGenerator.Gateway/Config/ocelot.development.json[m
[1mnew file mode 100644[m
[1mindex 0000000..b9c3718[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/Config/ocelot.development.json[m
[36m@@ -0,0 +1,71 @@[m
[32m+[m[32m{[m
[32m+[m[32m  "GlobalConfiguration": {[m
[32m+[m[32m    "BaseUrl": "http://localhost:5002"[m
[32m+[m[32m  },[m
[32m+[m[32m  "Routes": [[m
[32m+[m[32m    {[m
[32m+[m[32m      "DownstreamPathTemplate": "/usr{everything}",[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamHostAndPorts": [[m
[32m+[m[32m        {[m
[32m+[m[32m          "Host": "localhost",[m
[32m+[m[32m          "Port": 5010[m
[32m+[m[32m        }[m
[32m+[m[32m      ],[m
[32m+[m[32m      "UpstreamPathTemplate": "/usr{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        //LeastConnection ®C Ω´«Î«Û∑¢Õ˘◊Óø’œ–µƒƒ«∏ˆ∑˛ŒÒ∆˜[m
[32m+[m[32m        //RoundRobin ®C ¬÷¡˜∑¢ÀÕ[m
[32m+[m[32m        //NoLoadBalance ®C ◊‹ «∑¢Õ˘µ⁄“ª∏ˆ«Î«ÛªÚ’ﬂ «∑˛ŒÒ∑¢œ÷[m
[32m+[m[32m        //CookieStickySessions -–‘ª·ª∞¿‡–Õµƒ∏∫‘ÿ∆Ω∫‚[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    {[m
[32m+[m[32m      "DownstreamPathTemplate": "/maint{everything}",[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamHostAndPorts": [[m
[32m+[m[32m        {[m
[32m+[m[32m          "Host": "localhost",[m
[32m+[m[32m          "Port": 5020[m
[32m+[m[32m        }[m
[32m+[m[32m      ],[m
[32m+[m[32m      "UpstreamPathTemplate": "/maint{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    {[m
[32m+[m[32m      "DownstreamPathTemplate": "/cus{everything}",[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamHostAndPorts": [[m
[32m+[m[32m        {[m
[32m+[m[32m          "Host": "localhost",[m
[32m+[m[32m          "Port": 5030[m
[32m+[m[32m        }[m
[32m+[m[32m      ],[m
[32m+[m[32m      "UpstreamPathTemplate": "/cus{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    }[m
[32m+[m[32m  ][m
[32m+[m[32m}[m
[1mdiff --git a/CodeGenerator.Gateway/Config/ocelot.production.json b/CodeGenerator.Gateway/Config/ocelot.production.json[m
[1mnew file mode 100644[m
[1mindex 0000000..7e0f126[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/Config/ocelot.production.json[m
[36m@@ -0,0 +1,70 @@[m
[32m+[m[32m{[m
[32m+[m[32m  "GlobalConfiguration": {[m
[32m+[m[32m    "BaseUrl": "http://172.16.0.4:8888"[m
[32m+[m[32m  },[m
[32m+[m[32m  "Routes": [[m
[32m+[m[32m    {[m
[32m+[m[32m      "DownstreamPathTemplate": "/usr{everything}",[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamHostAndPorts": [[m
[32m+[m[32m        {[m
[32m+[m[32m          "Host": "172.16.0.4",[m
[32m+[m[32m          "Port": 9999[m
[32m+[m[32m        }[m
[32m+[m[32m      ],[m
[32m+[m[32m      "UpstreamPathTemplate": "/usr{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete",[m
[32m+[m[32m        "Options"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    {[m
[32m+[m[32m      "DownstreamPathTemplate": "/maint{everything}",[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamHostAndPorts": [[m
[32m+[m[32m        {[m
[32m+[m[32m          "Host": "172.16.0.4",[m
[32m+[m[32m          "Port": 9999[m
[32m+[m[32m        }[m
[32m+[m[32m      ],[m
[32m+[m[32m      "UpstreamPathTemplate": "/maint{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete",[m
[32m+[m[32m        "Options"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    {[m
[32m+[m[32m      "DownstreamPathTemplate": "/cus{everything}",[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamHostAndPorts": [[m
[32m+[m[32m        {[m
[32m+[m[32m          "Host": "172.16.0.4",[m
[32m+[m[32m          "Port": 9999[m
[32m+[m[32m        }[m
[32m+[m[32m      ],[m
[32m+[m[32m      "UpstreamPathTemplate": "/cus{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete",[m
[32m+[m[32m        "Options"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    }[m
[32m+[m[32m  ][m
[32m+[m[32m}[m
\ No newline at end of file[m
[1mdiff --git a/CodeGenerator.Gateway/Config/ocelot.test.json b/CodeGenerator.Gateway/Config/ocelot.test.json[m
[1mnew file mode 100644[m
[1mindex 0000000..ba84adb[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/Config/ocelot.test.json[m
[36m@@ -0,0 +1,87 @@[m
[32m+[m[32m//{ /*¡¨fabio*/[m
[32m+[m[32m  //"GlobalConfiguration": {[m
[32m+[m[32m  //  "BaseUrl": "http://localhost:8888"[m
[32m+[m[32m  //},[m
[32m+[m[32m  //"Routes": [[m
[32m+[m[32m  //  {[m
[32m+[m[32m  //    "DownstreamPathTemplate": "/sys{everything}",[m
[32m+[m[32m  //    "DownstreamScheme": "http",[m
[32m+[m[32m  //    "DownstreamHostAndPorts": [[m
[32m+[m[32m  //      {[m
[32m+[m[32m  //        "Host": "193.112.75.77",[m
[32m+[m[32m  //        "Port": 9999[m
[32m+[m[32m  //      }[m
[32m+[m[32m  //    ],[m
[32m+[m[32m  //    "UpstreamPathTemplate": "/sys{everything}",[m
[32m+[m[32m  //    "UpstreamHttpMethod": [[m
[32m+[m[32m  //      "Get",[m
[32m+[m[32m  //      "Put",[m
[32m+[m[32m  //      "Post",[m
[32m+[m[32m  //      "Delete",[m
[32m+[m[32m  //      "Options"[m
[32m+[m[32m  //    ],[m
[32m+[m[32m  //    "LoadBalancerOptions": {[m
[32m+[m[32m  //      "Type": "RoundRobin"[m
[32m+[m[32m  //    }[m
[32m+[m[32m  //  }[m
[32m+[m[32m  //][m
[32m+[m[32m//}[m
[32m+[m[32m/* ÷±¡¨consulµƒ≈‰÷√*/[m
[32m+[m[32m{[m
[32m+[m[32m  "GlobalConfiguration": {[m
[32m+[m[32m    "BaseUrl": "http://Õ¯πÿ∑˛ŒÒ∆˜IP:8888",[m
[32m+[m[32m    "ServiceDiscoveryProvider": {[m
[32m+[m[32m      "Scheme": "http",[m
[32m+[m[32m      "Host": "Consul∑˛ŒÒ∆˜IP",[m
[32m+[m[32m      "Port": 8550,[m
[32m+[m[32m      "Type": "Consul"[m
[32m+[m[32m    }[m
[32m+[m[32m  },[m
[32m+[m[32m  "Routes": [[m
[32m+[m[32m    {[m
[32m+[m[32m      "UpstreamPathTemplate": "/usr{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamPathTemplate": "/usr{everything}",[m
[32m+[m[32m      "ServiceName": "adnc.usr.webapi",[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    {[m
[32m+[m[32m      "UpstreamPathTemplate": "/maint{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamPathTemplate": "/maint{everything}",[m
[32m+[m[32m      "ServiceName": "adnc.maint.webapi",[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    {[m
[32m+[m[32m      "UpstreamPathTemplate": "/cus{everything}",[m
[32m+[m[32m      "UpstreamHttpMethod": [[m
[32m+[m[32m        "Get",[m
[32m+[m[32m        "Put",[m
[32m+[m[32m        "Post",[m
[32m+[m[32m        "Delete"[m
[32m+[m[32m      ],[m
[32m+[m[32m      "DownstreamScheme": "http",[m
[32m+[m[32m      "DownstreamPathTemplate": "/cus{everything}",[m
[32m+[m[32m      "ServiceName": "adnc.cus.webapi",[m
[32m+[m[32m      "LoadBalancerOptions": {[m
[32m+[m[32m        "Type": "RoundRobin"[m
[32m+[m[32m      }[m
[32m+[m[32m    }[m
[32m+[m[32m  ][m
[32m+[m[32m}[m
[1mdiff --git a/CodeGenerator.Gateway/Program.cs b/CodeGenerator.Gateway/Program.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..c81dae8[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/Program.cs[m
[36m@@ -0,0 +1,44 @@[m
[32m+[m[32musing Microsoft.AspNetCore.Hosting;[m
[32m+[m[32musing Microsoft.Extensions.Configuration;[m
[32m+[m[32musing Microsoft.Extensions.Hosting;[m
[32m+[m[32musing Microsoft.Extensions.Logging;[m
[32m+[m[32musing System;[m
[32m+[m[32musing System.Collections.Generic;[m
[32m+[m[32musing System.Linq;[m
[32m+[m[32musing System.Threading.Tasks;[m
[32m+[m[32musing CodeGenerator.Infra.Common.Extensions;[m
[32m+[m[32musing CodeGenerator.Infra.Common.Options;[m
[32m+[m
[32m+[m[32mnamespace CodeGenerator.Gateway[m
[32m+[m[32m{[m
[32m+[m[32m    public class Program[m
[32m+[m[32m    {[m
[32m+[m[32m        public static void Main(string[] args)[m
[32m+[m[32m        {[m
[32m+[m[32m            CreateHostBuilder(args).Build().Run();[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public static IHostBuilder CreateHostBuilder(string[] args) =>[m
[32m+[m[32m            Host.CreateDefaultBuilder(args)[m
[32m+[m[32m                .ConfigureAppConfiguration((context, cb) =>[m
[32m+[m[32m                {[m
[32m+[m[32m                    //…˙≤˙ª∑æ≥¥”consul≈‰÷√÷––ƒ∂¡»°≈‰÷√[m
[32m+[m[32m                    var env = context.HostingEnvironment;[m
[32m+[m[32m                    if (env.IsProduction() || env.IsStaging())[m
[32m+[m[32m                    {[m
[32m+[m[32m                        var configuration = cb.Build();[m
[32m+[m[32m                        var consulOption = configuration.GetSection("Consul").Get<ConsulOption>();[m
[32m+[m[32m                        cb.AddConsulConfiguration(new[] { consulOption.ConsulUrl }, consulOption.ConsulKeyPath);[m
[32m+[m[32m                    }[m
[32m+[m[32m                })[m
[32m+[m[32m                .ConfigureAppConfiguration((hostingContext, config) =>[m
[32m+[m[32m                {[m
[32m+[m[32m                    var env = hostingContext.HostingEnvironment;[m
[32m+[m[32m                    config.AddJsonFile($"{AppContext.BaseDirectory}/Config/ocelot.{env.EnvironmentName}.json", false, true);[m
[32m+[m[32m                })[m
[32m+[m[32m                .ConfigureWebHostDefaults(webBuilder =>[m
[32m+[m[32m                {[m
[32m+[m[32m                    webBuilder.UseStartup<Startup>();[m
[32m+[m[32m                });[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/CodeGenerator.Gateway/Properties/launchSettings.json b/CodeGenerator.Gateway/Properties/launchSettings.json[m
[1mnew file mode 100644[m
[1mindex 0000000..bed1a1c[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/Properties/launchSettings.json[m
[36m@@ -0,0 +1,27 @@[m
[32m+[m[32mÔªø{[m
[32m+[m[32m  "iisSettings": {[m
[32m+[m[32m    "windowsAuthentication": false,[m[41m [m
[32m+[m[32m    "anonymousAuthentication": true,[m[41m [m
[32m+[m[32m    "iisExpress": {[m
[32m+[m[32m      "applicationUrl": "http://localhost:5758",[m
[32m+[m[32m      "sslPort": 0[m
[32m+[m[32m    }[m
[32m+[m[32m  },[m
[32m+[m[32m  "profiles": {[m
[32m+[m[32m    "IIS Express": {[m
[32m+[m[32m      "commandName": "IISExpress",[m
[32m+[m[32m      "launchBrowser": true,[m
[32m+[m[32m      "environmentVariables": {[m
[32m+[m[32m        "ASPNETCORE_ENVIRONMENT": "Development"[m
[32m+[m[32m      }[m
[32m+[m[32m    },[m
[32m+[m[32m    "CodeGenerator.Geteway": {[m
[32m+[m[32m      "commandName": "Project",[m
[32m+[m[32m      "launchBrowser": true,[m
[32m+[m[32m      "applicationUrl": "http://localhost:5000",[m
[32m+[m[32m      "environmentVariables": {[m
[32m+[m[32m        "ASPNETCORE_ENVIRONMENT": "Development"[m
[32m+[m[32m      }[m
[32m+[m[32m    }[m
[32m+[m[32m  }[m
[32m+[m[32m}[m
[1mdiff --git a/CodeGenerator.Gateway/Startup.cs b/CodeGenerator.Gateway/Startup.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..fe18647[m
[1m--- /dev/null[m
[1m+++ b/CodeGenerator.Gateway/Startup.cs[m
[36m@@ -0,0 +1,67 @@[m
[32m+[m[32musing Microsoft.AspNetCore.Builder;[m
[32m+[m[32musing Microsoft.AspNetCore.Hosting;[m
[32m+[m[32musing Microsoft.AspNetCore.Http;[m
[32m+[m[32musing Microsoft.Extensions.Configuration;[m
[32m+[m[32musing Microsoft.Extensions.DependencyInjection;[m
[32m+[m[32musing Microsoft.Extensions.Hosting;[m
[32m+[m[32musing Ocelot.DependencyInjection;[m
[32m+[m[32musing Ocelot.Middleware;[m
[32m+[m[32musing Ocelot.Provider.Consul;[m
[32m+[m
[32m+[m[32mnamespace CodeGenerator.Gateway[m
[32m+[m[32m{[m
[32m+[m[32m    public class Startup[m
[32m+[m[32m    {[m
[32m+[m[32m        public IConfiguration Configuration { get; }[m
[32m+[m
[32m+[m[32m        public Startup(IConfiguration configuration)[m
[32m+[m[32m        {[m
[32m+[m[32m            Configuration = configuration;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        // This method gets called by the runtime. Use this method to add services to the container.[m
[32m+[m[32m    