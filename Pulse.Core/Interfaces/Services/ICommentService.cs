using Pulse.Core.DTOs;
using Pulse.Core.Feedback;

namespace Pulse.Core.Interfaces.Services;

public interface ICommentService
{
    ValueTask<ServiceResult<ReadPostDto>> AddComment(CreateCommentDto comment);
}
