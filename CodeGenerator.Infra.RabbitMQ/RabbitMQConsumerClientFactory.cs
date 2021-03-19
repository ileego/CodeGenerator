// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using CodeGenerator.Infra.RabbitMQ.Exceptions;
using CodeGenerator.Infra.RabbitMQ.Transport;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Infra.RabbitMQ
{
    internal sealed class RabbitMQConsumerClientFactory : IConsumerClientFactory
    {
        private readonly IConnectionChannelPool _connectionChannelPool;
        private readonly IOptions<RabbitMQOptions> _rabbitMqOptions;

        public RabbitMQConsumerClientFactory(IOptions<RabbitMQOptions> rabbitMqOptions, IConnectionChannelPool channelPool)
        {
            _rabbitMqOptions = rabbitMqOptions;
            _connectionChannelPool = channelPool;
        }

        public IConsumerClient Create(string groupId)
        {
            try
            {
                var client = new RabbitMQConsumerClient(groupId, _connectionChannelPool, _rabbitMqOptions);
                client.Connect();
                return client;
            }
            catch (System.Exception e)
            {
                throw new BrokerConnectionException(e);
            }
        }
    }
}