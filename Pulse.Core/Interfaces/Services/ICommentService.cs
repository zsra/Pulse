using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface ICommentService
{
    ValueTask<Response> AddComment(CreateCommentDto comment);
}
