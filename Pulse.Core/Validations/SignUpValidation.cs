using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

public class SignUpValidation : IValidation<SignUpDto>
{
    public bool IsValid(SignUpDto item, out List<ErrorMessage> errors)
    {
        errors = new List<ErrorMessage>();

        ValidateUsername(item.Username, errors);
        ValidatePassword(item.Password, item.RePassword, errors);
        ValidateEmail(item.Email, errors);
        
        return !errors.Any();
    }

    private static void ValidateUsername(string userName, List<ErrorMessage> errors)
    {
        if (!string.IsNullOrEmpty(userName))
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Username), "Username cannot be empty."));
        }

        if (userName.Length < 4)
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Username), "Length of Username is must be larger than 4 character"));
        }

        if (userName.Length > 16)
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Username), "Length of Username is must be smaller than 16 character"));
        }
    }

    private void ValidatePassword(string password, string rePassword, List<ErrorMessage> errors)
    {
        if(!string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Password), "Password cannot be empty."));
        }

        if(password.Length < 8)
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Password), "Password most be atleast 8 characters long."));
        }

        if (password != rePassword)
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Password), "Two password must be match."));
        }
    }

    private void ValidateEmail(string email, List<ErrorMessage> errors)
    {
        if (!string.IsNullOrEmpty(email))
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Email), "Email cannot be empty."));
        }

        if(!email.Contains('@'))
        {
            errors.Add(new ErrorMessage(nameof(SignUpDto.Email), "Email is invalid."));
        }
    }
}
