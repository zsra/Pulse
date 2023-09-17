using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Models;

public class Post : IModel
{
    public Post(string content, string creatorId)
    {
        Id = Guid.NewGuid().ToString();
        Content = content;
        CreatorId = creatorId;
    }

    public Post(string id, string content, uint likes, DateTime postedAt,
        string creatorId, IEnumerable<string> comments, IEnumerable<string> shared)
    {
        Id = id;
        Content = content;
        Likes = likes;
        PostedAt = postedAt;
        CreatorId = creatorId;
        Comments = comments;
        Shared = shared;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; }
    public required string Content { get; set; }
    public uint Likes { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime PostedAt { get; }

    public required string CreatorId { get; set; }
    public IEnumerable<string> Comments { get; set; } = new List<string>();
    public IEnumerable<string> Shared { get; set; } = new List<string>();
}
