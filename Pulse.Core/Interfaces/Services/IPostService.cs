using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface IPostService
{
    ValueTask<Response> CreatePostAsync(CreatePostDto post);
    ValueTask<Response> GetPostByIdAsync(string id);
    ValueTask<Response> DeletePostAsync(string id);
}
