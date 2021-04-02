using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infra.Common.Service
{
    public class AppServiceException : Exception
    {
        public AppServiceException(string message)
            : base(message)
        {
        }
        public AppServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
