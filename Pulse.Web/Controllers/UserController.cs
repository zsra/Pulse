using Microsoft.AspNetCore.Mvc;
using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
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
        Response response = new();
        
        try
        {
            response = await _userService.SignUpAsync(signUp);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            return new BadRequestObjectResult(response);
        }
    }

    [HttpPost("/signin")]
    public async ValueTask<IActionResult> SignIn([FromBody] SignInDto signIn)
    {
        Response response = new();

        try
        {
            response = await _userService.SignInAsync(signIn);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            return new BadRequestObjectResult(response);
        }
    }
}
