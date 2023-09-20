using Pulse.Core.DTOs;
using Pulse.Core.Extensions;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Interfaces.Validations;
using Pulse.Core.Models;

namespace Pulse.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userReposirory;
    private readonly IValidation<SignUpDto> _signUpValidation;

    public UserService(IUserRepository userReposirory, IValidation<SignUpDto> signUpValidation)
    {
        _userReposirory = userReposirory;
        _signUpValidation = signUpValidation;
    }

    public async ValueTask<Response> SignInAsync(string email, string password)
    {
        _ = email ?? throw new ArgumentNullException(nameof(email));
        _ = password ?? throw new ArgumentNullException(nameof(password));
        
        Response response = new();
        User user = await _userReposirory.GetUserByEmailAsync(email);

        if(user == null)
        {
            response.Messages.Add($"There are no user registered with {email}.");
            return response;
        }

        if(!BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
        {
            response.Messages.Add($"Incorrect password.");
            return response;
        }

        // TODO: return with session key

        return response;
    }

    public async ValueTask<Response> SignUpAsync(SignUpDto signUp)
    {
        _ = signUp ?? throw new ArgumentNullException(nameof(signUp));

        Response response = new();

        if(_signUpValidation.IsValid(signUp, ref response))
        {
            var user = signUp.SignUpToUser();
            await _userReposirory.CreateAsync(user);
        }

        response.ResponseType = Enums.ResponseTypes.Okay;

        return response;
    }
}
