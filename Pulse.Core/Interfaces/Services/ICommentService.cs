using Pulse.Core.DTOs;

namespace Pulse.Core.Interfaces.Services;

public interface ICommentService
{
    ValueTask<ReadPostDto> AddComment(CreateCommentDto comment);
    ValueTask<ReadPostDto> ReplyComment(CreateCommentDto comment);
}
