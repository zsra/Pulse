using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface IPostService
{
    ValueTask<ServiceResult<ReadPostDto>> CreatePostAsync(CreatePostDto post);
    ValueTask<ServiceResult<ReadPostDto>> GetPostByIdAsync(string id);
    ValueTask<ServiceResult<bool>> DeletePostAsync(string id, string creatorId);
}
