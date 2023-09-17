using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

internal interface IUserService
{
    ValueTask<Response> SignUpAsync(SignUpDto signUp);
    ValueTask<Response> SignInAsync(string email, string password);
}
