using Pulse.Core.DTOs;
using Pulse.Core.Models;

namespace Pulse.Core.Extensions;

public static class PostConverterExtensions
{
    public static Post CreatePostDtoToPost(this CreatePostDto post)
    {
        return new Post(post.Content!, post.CreatorId!);
    }

    public static ReadPostDto PostToReadPostDto(this Post post)
    {
        return new ReadPostDto()
        {
            PostedAt = post.PostedAt,
            Content = post.Content,
            CreatorId = post.CreatorId,
            Id = post.Id,
            Likes = post.Likes
        };
    }
}
