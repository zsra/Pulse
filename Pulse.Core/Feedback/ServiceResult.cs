namespace Pulse.Core.Feedback;

public class ServiceResult<T>
{
    public ServiceResult()
    {
    }

    public ServiceResult(T? data, List<ErrorMessage> errors)
    {
        Data = data;
        Errors = errors;
    }

    public T? Data { get; set; }
    public List<ErrorMessage> Errors { get; set; } 
        = new List<ErrorMessage>();
}
