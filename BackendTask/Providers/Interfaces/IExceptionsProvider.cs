using BackendTask.DataBase.Models;
using BackendTask.Models.Entities;
using Exception = BackendTask.DataBase.Models.Exception;

namespace BackendTask.Providers.Interfaces;

internal interface IExceptionsProvider
{
    Task<Exception> LogExceptionAsync(ExceptionType exceptionType, ExceptionData exceptionData);
}