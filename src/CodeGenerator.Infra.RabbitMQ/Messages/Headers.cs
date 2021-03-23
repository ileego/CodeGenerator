// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace CodeGenerator.Infra.RabbitMQ.Messages
{
    public static class Headers
    {
        /// <summary>
        /// Id of the message. Either set the ID explicitly when sending a message, or assign one to the message.
        /// </summary>
        public const string MessageId = "cg-msg-id";

        public const string MessageName = "cg-msg-name";

        public const string Group = "cg-msg-group";

        /// <summary>
        /// Message value .NET type
        /// </summary>
        public const string Type = "cg-msg-type";

        public const string CorrelationId = "cg-corr-id";

        public const string CorrelationSequence = "cg-corr-seq";

        public const string CallbackName = "cg-callback-name";

        public const string SentTime = "cg-senttime";

        public const string Exception = "cg-exception";
    }
}
