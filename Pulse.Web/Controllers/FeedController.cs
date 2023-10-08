using Microsoft.AspNetCore.Mvc;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController
{
    private readonly IFeedService _feedService;

    public FeedController(IFeedService feedService)
    {
        _feedService = feedService;
    }

    [HttpPost("/feed/{userId}/{page}/{pageSize}")]
    public async ValueTask<IActionResult> AddComment(string userId, int page = 1, int pageSize = 10)
    {
        try
        {
            return new OkObjectResult(await _feedService.GetFeed(userId, page, pageSize));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
