using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Core.Services
{
    public class PostService : IPostService
    {
        public ValueTask<Response> CreatePostAsync(CreatePostDto post)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Response> DeletePostAsync(string id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Response> GetPostByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
