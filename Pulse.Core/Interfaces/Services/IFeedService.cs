using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface IFeedService
{
    ValueTask<Response> GetFeed(string userId);
}
