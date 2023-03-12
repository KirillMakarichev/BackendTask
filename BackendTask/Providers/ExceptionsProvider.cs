using System.Diagnostics;
using BackendTask.DataBase;
using BackendTask.DataBase.Models;
using BackendTask.Models.Entities;
using BackendTask.Providers.Interfaces;
using Exception = BackendTask.DataBase.Models.Exception;

namespace BackendTask.Providers;

internal class ExceptionsProvider : IExceptionsProvider
{
    private readonly TreeContext _treeContext;

    public ExceptionsProvider(TreeContext treeContext)
    {
        _treeContext = treeContext;
    }
    
    public async Task<Exception> LogExceptionAsync(ExceptionType exceptionType, ExceptionData exceptionData)
    {
        var exception = new Exception()
        {
            ExceptionType = exceptionType,
            Data = new ExceptionData()
            {
                Message = exceptionData.Message,
                Body = exceptionData.Body,
                Headers = exceptionData.Headers,
                QueryParameters = exceptionData.QueryParameters,
                TraceId = exceptionData.TraceId
            },
            CreatedAt = Stopwatch.GetTimestamp()
        };
        _treeContext.Exceptions.Add(exception);

        await _treeContext.SaveChangesAsync();

        return exception;
    }
}