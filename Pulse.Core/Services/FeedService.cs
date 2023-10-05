using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Core.Services;

public class FeedService : IFeedService
{
    public async ValueTask<Response> GetFeed(string userId)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));
        Response response = new Response();

        return response;
    }
}
