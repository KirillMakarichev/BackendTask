using BackendTask.Models.Entities;

namespace BackendTask.Models.Routs.Responses;

internal class ExceptionResponse
{
    public long Id { get; set; }
    public ExceptionType ExceptionType { get; set; }
    public DateTime CreatedAt { get; set; }   
}