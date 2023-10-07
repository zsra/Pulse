using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Core.Services;

public class FeedService : IFeedService
{
    public ValueTask<ServiceResult<IReadOnlyList<ReadPostDto>>> GetFeed(string userId)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));
        throw new NotImplementedException();
    }
}
