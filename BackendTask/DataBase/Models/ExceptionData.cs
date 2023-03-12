using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTask.DataBase.Models;

[Table("exceptions_data")]
internal class ExceptionData
{
    public long Id { get; set; }
    public long ExceptionId { get; set; }
    public string Path { get; set; }
    public string Message { get; set; }
    public string TraceId { get; set; }
    public string[] QueryParameters { get; set; }
    public string[] Headers { get; set; }
    public string Body { get; set; }
}