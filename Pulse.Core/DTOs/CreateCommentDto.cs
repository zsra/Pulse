namespace Pulse.Core.DTOs;

public class CreateCommentDto
{
    public string? PostId { get; set; }
    public string? ReplyId { get; set; }
    public string? CreatorId { get; set; }
    public string? Content { get; set; }
}
