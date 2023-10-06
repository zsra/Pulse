using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface IUserService
{
    ValueTask<ServiceResult<bool>> SignUpAsync(SignUpDto signUp);
    ValueTask<ServiceResult<string>> SignInAsync(SignInDto signIn);
}
