using Pulse.Core.DTOs;
using Pulse.Core.Interfaces.Services;

namespace Pulse.Core.Services
{
    public class CommentService : ICommentService
    {
        public ValueTask<ReadPostDto> AddComment(CreateCommentDto comment)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ReadPostDto> ReplyComment(CreateCommentDto comment)
        {
            throw new NotImplementedException();
        }
    }
}
