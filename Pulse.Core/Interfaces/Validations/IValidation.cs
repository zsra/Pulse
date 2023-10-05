using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Validations;

public interface IValidation<T> where T : class
{
    bool IsValid(T item, out List<ErrorMessage> errors);
}
