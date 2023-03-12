using BackendTask.DataBase;
using BackendTask.DataBase.Models;
using BackendTask.Models.Entities;
using BackendTask.Models.Routs.Requests;
using BackendTask.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
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
                TraceId = exceptionData.TraceId,
                Path = exceptionData.Path
            },
            CreatedAt = DateTime.UtcNow
        };
        _treeContext.Exceptions.Add(exception);

        await _treeContext.SaveChangesAsync();

        return exception;
    }

    public async Task<(int count, List<Exception>)> GetExceptionsAsync(int take, int skip, JournalGetRequest filter)
    {
        var exceptions = _treeContext.Exceptions.OrderByDescending(x => x.CreatedAt).AsQueryable();

        if (filter.From > DateTime.MinValue)
            exceptions = exceptions.Where(x => x.CreatedAt >= filter.From);
        
        if (filter.To > DateTime.MinValue)
            exceptions = exceptions.Where(x => x.CreatedAt <= filter.From);

        if (!string.IsNullOrWhiteSpace(filter.Search))
            exceptions = exceptions.Where(x => x.Data.Message.ToLower().Contains(filter.Search.ToLower()));

        var count = await exceptions.CountAsync();

        exceptions = exceptions.Skip(skip);

        take = take == 0 ? 10 : take;

        exceptions = exceptions.Take(take);
        
        var filteredExceptions = await exceptions.ToListAsync();
        
        return (count, filteredExceptions);
    }

    public async Task<ExceptionData> GetExceptionAsync(long id)
    {
        var exceptionData = await _treeContext.ExceptionData.SingleAsync(x => x.Id == id);

        return exceptionData;
    }
}