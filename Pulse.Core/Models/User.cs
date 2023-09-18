using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Models;

public class User : IModel
{
    public User(string name, string email, string username, string password, DateTime birthday)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        Username = username;
        HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        CreatedAt = DateTime.UtcNow;
        Birthday = birthday;
    }

    public User(string id, string name, string email, string username,
        string password, DateTime createdAt, DateTime birthday,
        IEnumerable<string> roles, IEnumerable<string> posts, IEnumerable<string> followed,
        IEnumerable<string> followers, IEnumerable<string> likes)
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
        HashedPassword = password;
        CreatedAt = createdAt;
        Birthday = birthday;
        Roles = roles;
        Posts = posts;
        Followed = followed;
        Followers = followers;
        Likes = likes;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Birthday { get; set; }

    public IEnumerable<string> Roles { get; set; } = new List<string>() { nameof(Enums.Roles.USER) };
    public IEnumerable<string> Posts { get; set; } = new List<string>();
    public IEnumerable<string> Followed { get; set; } = new List<string>();
    public IEnumerable<string> Followers { get; set; } = new List<string>();
    public IEnumerable<string> Likes { get; set; } = new List<string>();
}
