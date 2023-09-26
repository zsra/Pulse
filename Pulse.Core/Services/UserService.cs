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
    private readonly IValidation<SignInDto> _signInValidation;

    public UserService(IUserRepository userReposirory,
        IValidation<SignUpDto> signUpValidation, IValidation<SignInDto> signInValidation)
    {
        _userReposirory = userReposirory;
        _signUpValidation = signUpValidation;
        _signInValidation = signInValidation;
    }

    public async ValueTask<Response> SignInAsync(SignInDto signIn)
    {
        _ = signIn ?? throw new ArgumentNullException(nameof(signIn));
        
        Response response = new();

        if(!_signInValidation.IsValid(signIn, ref response))
        {
            return response;
        }

        User user = await _userReposirory.GetUserByEmailAsync(signIn.Email!);

        if(user == null)
        {
            response.Messages.Add($"There are no user registered with {signIn.Email}.");
            return response;
        }

        if(!BCrypt.Net.BCrypt.Verify(signIn.Password, user.HashedPassword))
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
