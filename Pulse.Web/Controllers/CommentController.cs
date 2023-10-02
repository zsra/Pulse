using Microsoft.AspNetCore.Mvc;
using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> AddComment([FromBody] CreateCommentDto comment)
    {
        Response response = new();

        try
        {
            response = await _commentService.AddComment(comment);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            return new BadRequestObjectResult(response);
        }
    }
}
