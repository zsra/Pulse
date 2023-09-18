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
    private readonly IRepository<User> _userReposirory;
    private readonly IValidation<SignUpDto> _signUpValidation;

    public UserService(IRepository<User> userReposirory, IValidation<SignUpDto> signUpValidation)
    {
        _userReposirory = userReposirory;
        _signUpValidation = signUpValidation;
    }

    public ValueTask<Response> SignInAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<Response> SignUpAsync(SignUpDto signUp)
    {
        _ = signUp ?? throw new ArgumentNullException(nameof(signUp));

        Response response = new();

        if(_signUpValidation.IsValid(signUp, ref response))
        {
            var user = UserConverterExtensions.SignUpToUser(signUp);
            await _userReposirory.Create(user);
        }

        response.ResponseType = Enums.ResponseTypes.Okay;

        return response;
    }
}
