using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface IFeedService
{
    ValueTask<ServiceResult<IReadOnlyList<ReadPostDto>>> GetFeed(string userId);
}
