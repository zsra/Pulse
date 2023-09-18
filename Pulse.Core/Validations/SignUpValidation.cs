using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

public class SignUpValidation : IValidation<SignUpDto>
{
    public bool IsValid(SignUpDto signUp, ref Response response)
    {
        ValidateUsername(signUp.Username, response);
        ValidatePassword(signUp.Password, signUp.RePassword, response);
        ValidateEmail(signUp.Email, response);

        return !response.Messages.Any();
    }

    private static void ValidateUsername(string userName, Response response)
    {
        if (!string.IsNullOrEmpty(userName))
        {
            response.Messages.Add("Username cannot be empty.");
        }

        if (userName.Length < 4)
        {
            response.Messages.Add("Length of Username is must be larger than 4 character");
        }

        if (userName.Length > 16)
        {
            response.Messages.Add("Length of Username is must be smaller than 16 character");
        }
    }

    private void ValidatePassword(string password, string rePassword, Response response)
    {
        if(!string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
        {
            response.Messages.Add("Password cannot be empty.");
        }

        if(password.Length < 8)
        {
            response.Messages.Add("Password most be atleast 8 characters long.");
        }

        if (password != rePassword)
        {
            response.Messages.Add("Two password must be match.");
        }
    }

    private void ValidateEmail(string email, Response response)
    {
        if (!string.IsNullOrEmpty(email))
        {
            response.Messages.Add("email cannot be empty.");
        }

        if(!email.Contains('@'))
        {
            response.Messages.Add("email is invalid.");
        }
    }
}
