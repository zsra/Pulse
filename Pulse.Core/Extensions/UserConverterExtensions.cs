using Pulse.Core.DTOs;
using Pulse.Core.Models;

namespace Pulse.Core.Extensions;

public static class UserConverterExtensions
{
    public static User SignUpToUser(SignUpDto signUp)
    {
        return new User(
            signUp.Username,
            signUp.Email,
            signUp.Username,
            signUp.Password,
            signUp.Birthday
        );
    }
}
