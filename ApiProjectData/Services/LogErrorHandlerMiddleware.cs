using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ApiProjectData.Services;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
// Вам может понадобиться установить пакет Microsoft.AspNetCore.Http.Abstractions в ваш проект
public class LogErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogErrorHandlerMiddleware> _logger;

    public LogErrorHandlerMiddleware(RequestDelegate next, ILogger<LogErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unknown error!");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Error = StatusCodes.Status500InternalServerError.ToString() });
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
// Метод расширения, используемый для добавления промежуточного программного обеспечения в конвейер HTTP-запросов.
public static class LogErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseLogErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogErrorHandlerMiddleware>();
    }
}