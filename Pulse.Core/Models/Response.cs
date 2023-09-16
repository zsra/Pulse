using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pulse.Core.Models;

public class Response
{
    public Response(string content, string postId, string creatorId)
    {
        Id = Guid.NewGuid().ToString();
        Content = content;
        PostId = postId;
        CreatorId = creatorId;
        RespondedAt = DateTime.UtcNow;
    }

    public Response(string id, string content, uint likes, DateTime respondedAt, 
        string postId, string creatorId, IEnumerable<string> responses, IEnumerable<string> shared)
    {
        Id = id;
        Content = content;
        Likes = likes;
        RespondedAt = respondedAt;
        PostId = postId;
        CreatorId = creatorId;
        Responses = responses;
        Shared = shared;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; }
    public required string Content { get; set; }
    public uint Likes { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime RespondedAt { get; }

    public required string PostId { get; set; }
    public required string CreatorId { get; set; }
    public IEnumerable<string> Responses { get; set; } = new List<string>();
    public IEnumerable<string> Shared { get; set; } = new List<string>();
}
