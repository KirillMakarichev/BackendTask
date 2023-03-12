namespace BackendTask.Models.Routs.Requests;

public class JournalGetRequest
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string? Search { get; set; }
}