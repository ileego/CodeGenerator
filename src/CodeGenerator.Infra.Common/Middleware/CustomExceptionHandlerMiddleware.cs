using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeGenerator.Infra.Common.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next
            , IWebHostEnvironment env
            , ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var eventId = new EventId(exception.HResult);
            _logger.LogError(eventId, exception, exception.Message);

            const int status = 500;
            var type = string.Concat("https://httpstatuses.com/", status);
            var title = _env.IsDevelopment() ? exception.Message : "系统异常";
            var exceptionDetail = _env.IsDevelopment() ? ExceptionHelper.GetExceptionDetail(exception) : $"系统异常,请联系管理员({eventId})";

            var problemDetails = new ProblemDetails
            {
                Title = title
                ,
                Detail = exceptionDetail
                ,
                Type = type
                ,
                Status = status
            };

            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";
            var errorText = JsonSerializer.Serialize(problemDetails, SystemTextJsonHelper.GetDefaultOptions());
            await context.Response.WriteAsync(errorText);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
