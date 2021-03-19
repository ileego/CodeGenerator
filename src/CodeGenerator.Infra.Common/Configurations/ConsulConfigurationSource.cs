using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CodeGenerator.Infra.Common.Configurations
{
    public class ConsulConfigurationSource : IConfigurationSource
    {
        public IEnumerable<Uri> ConsulUrls { get; }
        public string Path { get; }

        public ConsulConfigurationSource(IEnumerable<Uri> consulUrls, string path)
        {
            ConsulUrls = consulUrls;
            Path = path;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(ConsulUrls, Path);
        }
    }
}
