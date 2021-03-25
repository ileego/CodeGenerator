using CodeGenerator.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace UnitTest.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        public IConfiguration Configuration { get; private set; }
        private readonly TestServer _testServer;
        private HttpClient Client;
        public TestServerFixture()
        {
            var basePath = ApplicationEnvironment.ApplicationBasePath;
            Configuration = new ConfigurationBuilder().AddJsonFile($"{basePath}appsettings.json", false, true).Build();

            var build = new WebHostBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>();
            _testServer = new TestServer(build);
            Client = _testServer.CreateClient();
        }
        public void Dispose()
        {
            Client?.Dispose();
            _testServer?.Dispose();
        }
    }
}
