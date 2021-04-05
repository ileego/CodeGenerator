using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.Filters;

namespace CodeGenerator.Infra.Common.Service
{
    public class AppServiceException : Exception, IException
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
