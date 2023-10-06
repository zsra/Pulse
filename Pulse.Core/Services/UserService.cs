using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pulse.Core.DTOs;
using Pulse.Core.Extensions;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Interfaces.Validations;
using Pulse.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pulse.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userReposirory;
    private readonly IValidation<SignUpDto> _signUpValidation;
    private readonly IValidation<SignInDto> _signInValidation;
    private readonly IConfiguration _config;

    public UserService(IUserRepository userReposirory, IValidation<SignUpDto> signUpValidation,
        IValidation<SignInDto> signInValidation, IConfiguration config)
    {
        _userReposirory = userReposirory;
        _signUpValidation = signUpValidation;
        _signInValidation = signInValidation;
        _config = config;
    }

    public async ValueTask<ServiceResult<string>> SignInAsync(SignInDto signIn)
    {
        _ = signIn ?? throw new ArgumentNullException(nameof(signIn));

        ServiceResult<string> result = new();

        if (!_signInValidation.IsValid(signIn, out List<ErrorMessage> errors))
        {
            result.Errors = errors;
            return result;
        }

        User user = await _userReposirory.GetUserByEmailAsync(signIn.Email!);

        if(user == null)
        {
            result.Errors.Add(new ErrorMessage(nameof(signIn.Email), $"There are no user registered with {signIn.Email}."));
            return result;
        }

        if(!BCrypt.Net.BCrypt.Verify(signIn.Password, user.HashedPassword))
        {
            result.Errors.Add(new ErrorMessage(nameof(signIn.Password), $"Incorrect password."));
            return result;
        }

        result.Data = GenerateToken(user);

        return result;
    }

    public async ValueTask<ServiceResult<bool>> SignUpAsync(SignUpDto signUp)
    {
        _ = signUp ?? throw new ArgumentNullException(nameof(signUp));

        ServiceResult<bool> result = new();

        if (!_signUpValidation.IsValid(signUp, out List<ErrorMessage> errors))
        {
            result.Errors = errors;
            return result;
        }

        var user = signUp.SignUpToUser();
        await _userReposirory.CreateAsync(user);

        return result;
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Roles.First())
            };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
