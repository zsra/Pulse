namespace Pulse.Core.Feedback;

public class ErrorMessage
{
    public ErrorMessage(string id, string message)
    {
        Id = id;
        Message = message;
    }

    public string Id { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
