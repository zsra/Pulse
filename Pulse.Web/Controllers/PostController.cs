using Microsoft.AspNetCore.Mvc;
using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("/{id}")]
    public async ValueTask<IActionResult> GetPostById(string id)
    {
        Response response = new();

        try
        {
            response = await _postService.GetPostByIdAsync(id);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            return new BadRequestObjectResult(response);
        }
    }

    [HttpPost("/create")]
    public async ValueTask<IActionResult> Create([FromBody] CreatePostDto post)
    {
        Response response = new();

        try
        {
            response = await _postService.CreatePostAsync(post);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            return new BadRequestObjectResult(response);
        }
    }

    [HttpDelete("/delete/{creatorId}/{postId}")]
    public async ValueTask<IActionResult> Delete(string creatorId, string postId)
    {
        Response response = new();

        try
        {
            response = await _postService.DeletePostAsync(postId, creatorId);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            return new BadRequestObjectResult(response);
        }
    }
}
