// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CodeGenerator.Infra.RabbitMQ.Messages;
using CodeGenerator.Infra.RabbitMQ.Transport;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Headers = CodeGenerator.Infra.RabbitMQ.Messages.Headers;

namespace CodeGenerator.Infra.RabbitMQ
{
    internal sealed class RabbitMQConsumerClient : IConsumerClient
    {
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);

        private readonly IConnectionChannelPool _connectionChannelPool;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly RabbitMQOptions _rabbitMqOptions;
        private IModel _channel;

        private IConnection _connection;

        public RabbitMQConsumerClient(string queueName,
            IConnectionChannelPool connectionChannelPool,
            IOptions<RabbitMQOptions> options)
        {
            _queueName = queueName;
            _connectionChannelPool = connectionChannelPool;
            _rabbitMqOptions = options.Value;
            _exchangeName = connectionChannelPool.Exchange;
        }

        public event EventHandler<TransportMessage> OnMessageReceived;

        public event EventHandler<LogMessageEventArgs> OnLog;

        public BrokerAddress BrokerAddress => new BrokerAddress("RabbitMQ", _rabbitMqOptions.HostName);

        public void Subscribe(IEnumerable<string> topics)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(nameof(topics));
            }

            Connect();

            foreach (var topic in topics)
            {
                _channel.QueueBind(_queueName, _exchangeName, topic);
            }
        }

        public void Listening(TimeSpan timeout, CancellationToken cancellationToken)
        {
            Connect();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnConsumerReceived;
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                cancellationToken.WaitHandle.WaitOne(timeout);
            }

            // ReSharper disable once FunctionNeverReturns
        }

        public void Commit(object sender)
        {
            _channel.BasicAck((ulong)sender, false);
        }

        public void Reject(object sender)
        {
            _channel.BasicReject((ulong)sender, true);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public void Connect()
        {
            if (_connection != null)
            {
                return;
            }

            _connectionLock.Wait();

            try
            {
                if (_connection == null)
                {
                    _connection = _connectionChannelPool.GetConnection();

                    _channel = _connection.CreateModel();

                    _channel.ExchangeDeclare(_exchangeName, RabbitMQOptions.ExchangeType, true);

                    var arguments = new Dictionary<string, object>
                    {
                        {"x-queue-mode", _rabbitMqOptions.QueueArguments.QueueMode},
                        {"x-message-ttl", _rabbitMqOptions.QueueArguments.MessageTTL}
                    };
                    _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: arguments);
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        #region events

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            var args = new LogMessageEventArgs
            {
                LogType = MqLogType.ConsumerCancelled,
                Reason = string.Join(",", e.ConsumerTags)
            };
            OnLog?.Invoke(sender, args);
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            var args = new LogMessageEventArgs
            {
                LogType = MqLogType.ConsumerUnregistered,
                Reason = string.Join(",", e.ConsumerTags)
            };
            OnLog?.Invoke(sender, args);
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            var args = new LogMessageEventArgs
            {
                LogType = MqLogType.ConsumerRegistered,
                Reason = string.Join(",", e.ConsumerTags)
            };
            OnLog?.Invoke(sender, args);
        }

        private void OnConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var headers = new Dictionary<string, string>();
            foreach (var header in e.BasicProperties.Headers)
            {
                headers.Add(header.Key, header.Value == null ? null : Encoding.UTF8.GetString((byte[])header.Value));
            }
            headers.Add(Headers.Group, _queueName);

            var message = new TransportMessage(headers, e.Body.ToArray());

            OnMessageReceived?.Invoke(e.DeliveryTag, message);
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            var args = new LogMessageEventArgs
            {
                LogType = MqLogType.ConsumerShutdown,
                Reason = e.ReplyText
            };

            OnLog?.Invoke(sender, args);
        }

        #endregion
    }
}