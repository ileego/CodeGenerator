﻿// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace CodeGenerator.Infra.RabbitMQ.Transport
{
    /// <summary>
    /// Consumer client factory to create consumer client instance.
    /// </summary>
    public interface IConsumerClientFactory
    {
        /// <summary>
        /// Create a new instance of <see cref="IConsumerClient" />.
        /// </summary>
        /// <param name="groupId">message group number</param>
        IConsumerClient Create(string groupId);
    }
}