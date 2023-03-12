using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTask.DataBase.Models;

[Table("exceptions_data")]
internal class ExceptionData
{
    public long Id { get; set; }
    public string Message { get; set; }
}