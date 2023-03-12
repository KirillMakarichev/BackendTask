using System.ComponentModel.DataAnnotations.Schema;
using BackendTask.Models.Entities;

namespace BackendTask.DataBase.Models;

[Table("exceptions")]
internal class Exception
{
    public long Id { get; set; }
    public ExceptionType ExceptionType { get; set; }
    public long CreatedAt { get; set; }
    public ExceptionData Data { get; set; }
}