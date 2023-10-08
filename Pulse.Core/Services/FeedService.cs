using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Core.Services;

public class FeedService : IFeedService
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public FeedService(IUserRepository userRepository, IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async ValueTask<ServiceResult<IReadOnlyList<ReadPostDto>>> GetFeed(string userId, int page = 1, int pageSize = 10)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));
        ServiceResult<IReadOnlyList<ReadPostDto>> result = new();

        var user = await _userRepository.GetByIdAsync(userId);

        int skip = (page - 1) * pageSize;

        var posts = await _postRepository.GetAllAsync();

        result.Data = (IReadOnlyList<ReadPostDto>?)posts.Where(p => user.Followed.Contains(p.CreatorId))
            .OrderBy(p => p.PostedAt)
            .Skip(skip)
            .Take(pageSize)
            .ToList();

        return result;
    }
}
