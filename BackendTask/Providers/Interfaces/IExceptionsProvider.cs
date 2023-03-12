using BackendTask.DataBase.Models;
using BackendTask.Models.Entities;
using BackendTask.Models.Routs.Requests;
using Exception = BackendTask.DataBase.Models.Exception;

namespace BackendTask.Providers.Interfaces;

internal interface IExceptionsProvider
{
    Task<Exception> LogExceptionAsync(ExceptionType exceptionType, ExceptionData exceptionData);
    Task<(int count, List<Exception>)> GetExceptionsAsync(int take, int skip, JournalGetRequest filter);
    Task<ExceptionData> GetExceptionAsync(long id);

}