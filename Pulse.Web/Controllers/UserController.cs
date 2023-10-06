using Microsoft.AspNetCore.Mvc;
using Pulse.Core.DTOs;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/signup")]
    public async ValueTask<IActionResult> SignUp([FromBody] SignUpDto signUp)
    {
        try
        {
            return new OkObjectResult(await _userService.SignUpAsync(signUp));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpPost("/signin")]
    public async ValueTask<IActionResult> SignIn([FromBody] SignInDto signIn)
    {
        try
        {
            return new OkObjectResult(await _userService.SignInAsync(signIn));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
