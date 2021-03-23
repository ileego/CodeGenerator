// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using CodeGenerator.Infra.RabbitMQ.Transport;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Infra.RabbitMQ
{
    internal sealed class RabbitMQExtension
    {
        private readonly Action<RabbitMQOptions> _configure;

        public RabbitMQExtension(Action<RabbitMQOptions> configure)
        {
            _configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {

            services.Configure(_configure);
            services.AddSingleton<ITransport, RabbitMQTransport>();
            services.AddSingleton<IConsumerClientFactory, RabbitMQConsumerClientFactory>();
            services.AddSingleton<IConnectionChannelPool, ConnectionChannelPool>();
        }
    }
}