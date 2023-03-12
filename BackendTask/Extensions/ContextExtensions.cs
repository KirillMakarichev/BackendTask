using BackendTask.DataBase.Models;

namespace BackendTask.Extensions;

internal static class ContextExtensions
{
    public static async Task<ExceptionData> CastToExceptionData(this HttpContext context, string message)
    {
        var request = context.Request;
        return new ExceptionData()
        {
            Body = await new StreamReader(context.Request.Body).ReadToEndAsync(),
            Headers = request.Headers.Select(x => $"{x.Key}: {x.Value}").ToArray(),
            QueryParameters = request.Query.Select(x => $"{x.Key}: {x.Value}").ToArray(),
            TraceId = context.TraceIdentifier,
            Message = message
        };
    }
}