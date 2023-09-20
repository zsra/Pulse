using Pulse.Core.Interfaces;

namespace Pulse.Core.DTOs;

public class SignUpDto
{
    public SignUpDto(string username, string password,
        string rePassword, string email, DateTime birthday)
    {
        Username = username;
        Password = password;
        RePassword = rePassword;
        Email = email;
        Birthday = birthday;
    }

    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string RePassword { get; set; }
    public required string Email { get; set; }
    public required DateTime Birthday { get; set; }
}
