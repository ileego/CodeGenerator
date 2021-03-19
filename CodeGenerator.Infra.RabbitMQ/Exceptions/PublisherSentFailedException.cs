using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infra.RabbitMQ.Exceptions
{
    public class PublisherSentFailedException : Exception
    {
        public PublisherSentFailedException(string message) : base(message)
        {
        }

        public PublisherSentFailedException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
