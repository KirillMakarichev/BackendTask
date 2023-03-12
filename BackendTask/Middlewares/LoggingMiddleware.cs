using BackendTask.Extensions;
using BackendTask.Models.Entities;
using BackendTask.Providers.Interfaces;
using Exception = System.Exception;

namespace BackendTask.Middlewares;

internal class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IExceptionsProvider exceptionsProvider)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (Exception e)
        {
            var data = await context.CastToExceptionData(e.Message);

            var exception = await exceptionsProvider.LogExceptionAsync(ExceptionType.Exception, data);

            var response = context.Response;
            
            response.StatusCode = 500;
            await response.WriteAsJsonAsync(new
            {
                type = ExceptionType.Exception.ToString(),
                id = exception.Id,
                data = new
                {
                    message = $"Internal server error ID = {exception.Id}."
                }
            });
        }
    }
}