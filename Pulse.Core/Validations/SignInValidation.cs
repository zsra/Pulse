using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

internal class SignInValidation : IValidation<SignInDto>
{
    public bool IsValid(SignInDto item, ref Response response)
    {
        if (!string.IsNullOrEmpty(item.Email))
        {
            response.Messages.Add("Username cannot be empty.");
        }

        if (!string.IsNullOrEmpty(item.Password))
        {
            response.Messages.Add("Username cannot be empty.");
        }

        return response.Messages.Any();
    }
}
