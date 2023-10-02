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

        response.Content = GenerateToken(user);

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
