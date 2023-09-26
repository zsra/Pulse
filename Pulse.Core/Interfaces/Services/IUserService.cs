using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface IUserService
{
    ValueTask<Response> SignUpAsync(SignUpDto signUp);
    ValueTask<Response> SignInAsync(SignInDto signIn);
}
