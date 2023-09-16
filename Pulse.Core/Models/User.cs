using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pulse.Core.Models;

public class User
{
    public User(string name, string email, string username, string password)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        Username = username;
        HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        CreatedAt = DateTime.UtcNow;
    }

    public User(string id, string name, string email, string username, string password, DateTime createdAt,
        IEnumerable<string> roles, IEnumerable<string> posts, IEnumerable<string> followed,
        IEnumerable<string> followers, IEnumerable<string> likes, IEnumerable<string> shared)
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
        HashedPassword = password;
        CreatedAt = createdAt;
        Roles = roles;
        Posts = posts;
        Followed = followed;
        Followers = followers;
        Likes = likes;
        Shared = shared;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string HashedPassword { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; }
    
    public IEnumerable<string> Roles { get; set; } = new List<string>() { nameof(Enums.Roles.USER) };
    public IEnumerable<string> Posts { get; set; } = new List<string>();
    public IEnumerable<string> Followed { get; set; } = new List<string>();
    public IEnumerable<string> Followers { get; set; } = new List<string>();
    public IEnumerable<string> Likes { get; set; } = new List<string>();
    public IEnumerable<string> Shared { get; set; } = new List<string>();
}
