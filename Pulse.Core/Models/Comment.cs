using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Models;

public class Comment : IModel
{
    public Comment(string content, string postId, string creatorId)
    {
        Id = Guid.NewGuid().ToString();
        Content = content;
        PostId = postId;
        CreatorId = creatorId;
        CommentedAt = DateTime.UtcNow;
    }

    public Comment(string id, string content, uint likes, DateTime commentedAt, 
        string postId, string creatorId, IEnumerable<string> replies, IEnumerable<string> shared)
    {
        Id = id;
        Content = content;
        Likes = likes;
        CommentedAt = commentedAt;
        PostId = postId;
        CreatorId = creatorId;
        Replies = replies;
        Shared = shared;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; }
    public string Content { get; set; }
    public uint Likes { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CommentedAt { get; }

    public string PostId { get; set; }
    public string CreatorId { get; set; }
    public IEnumerable<string> Replies { get; set; } = new List<string>();
    public IEnumerable<string> Shared { get; set; } = new List<string>();
}
