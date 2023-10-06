using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

public class SignInValidation : IValidation<SignInDto>
{
    public bool IsValid(SignInDto item, out List<ErrorMessage> errors)
    {
        errors = new List<ErrorMessage>();

        if (!string.IsNullOrEmpty(item.Email))
        {
            errors.Add(new ErrorMessage(nameof(item.Email), "Username cannot be empty."));
        }

        if (!string.IsNullOrEmpty(item.Password))
        {
            errors.Add(new ErrorMessage(nameof(item.Password), "Username cannot be empty."));
        }

        return errors.Any();
    }
}
