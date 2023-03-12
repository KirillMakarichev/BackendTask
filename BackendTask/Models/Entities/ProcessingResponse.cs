namespace BackendTask.Models.Entities;

internal class ProcessingResponse
{
    public bool Success { get; }
    public string Message { get; }

    public ProcessingResponse(string message)
    {
        Message = message;
        Success = false;
    }

    public ProcessingResponse()
    {
        Success = true;
    }
}