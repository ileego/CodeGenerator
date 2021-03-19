using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.Options;
using Microsoft.Extensions.Configuration;

namespace CodeGenerator.Infra.Common.Configurations
{
    public class DefaultConsulConfigurationSource : IConfigurationSource
    {
        private readonly ConsulOption _consulOption;
        private readonly bool _reloadOnChanges;

        public DefaultConsulConfigurationSource(ConsulOption consulOption, bool reloadOnChanges)
        {
            _consulOption = consulOption;
            _reloadOnChanges = reloadOnChanges;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DefaultConsulConfigurationProvider(_consulOption, _reloadOnChanges);
        }
    }
}
