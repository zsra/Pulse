using Microsoft.AspNetCore.Mvc;
using Pulse.Core.DTOs;
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
        try
        {
            return new OkObjectResult(await _postService.GetPostByIdAsync(id));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpPost("/create")]
    public async ValueTask<IActionResult> Create([FromBody] CreatePostDto post)
    {
        try
        {
            return new OkObjectResult(await _postService.CreatePostAsync(post));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpDelete("/delete/{creatorId}/{postId}")]
    public async ValueTask<IActionResult> Delete(string creatorId, string postId)
    {
        try
        {
            return new OkObjectResult(await _postService.DeletePostAsync(postId, creatorId));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
