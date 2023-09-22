using Pulse.Core.DTOs;
using Pulse.Core.Models;

namespace Pulse.Core.Extensions;

public static class CommentConverterExtenstions
{
    public static Comment CreateCommentDtoToComment(this CreateCommentDto comment)
    {
        return new Comment(comment.Content!, comment.PostId!, comment.CreatorId!);
    }
}
