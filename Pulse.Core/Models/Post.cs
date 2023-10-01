using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Models;

public class Post : IModel
{
    public Post(string content, string creatorId)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Content = content;
        CreatorId = creatorId;
    }

    public Post(string id, string content, int likes, DateTime postedAt,
        string creatorId, ICollection<string> comments, ICollection<string> shared)
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
    public string Content { get; set; }
    public int Likes { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime PostedAt { get; }

    public string CreatorId { get; set; }
    public ICollection<string> Comments { get; set; } = new List<string>();
    public ICollection<string> Shared { get; set; } = new List<string>();
}
