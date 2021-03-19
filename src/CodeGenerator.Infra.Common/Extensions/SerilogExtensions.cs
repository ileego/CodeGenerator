using Microsoft.AspNetCore.Builder;
using System;
using CodeGenerator.Infra.Common.Middleware;

namespace CodeGenerator.Infra.Common.Extensions
{
    public static class SerilogExtensions
    {
        public static IApplicationBuilder UseSerilog(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<SerilogMiddleware>();
        }
    }
}
