using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Models;

namespace Pulse.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userReposirory;

    public UserService(IRepository<User> userReposirory)
    {
        _userReposirory = userReposirory;
    }

    public ValueTask<Response> SignInAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Response> SignUpAsync(SignUpDto signUp)
    {
        _ = signUp ?? throw new ArgumentNullException(nameof(signUp));
    }
}
