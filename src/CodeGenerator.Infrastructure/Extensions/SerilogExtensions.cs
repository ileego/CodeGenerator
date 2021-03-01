using CodeGenerator.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;

namespace CodeGenerator.Infrastructure.Extensions
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
