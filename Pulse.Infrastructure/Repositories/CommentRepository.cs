using MongoDB.Bson;
using MongoDB.Driver;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Models;
using Pulse.Core.Settings;
using Pulse.Infrastructure.Attributes;

namespace Pulse.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IMongoCollection<Comment> _collection;

    public CommentRepository(IMongoSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<Comment>(GetCollectionName(typeof(Comment)));
    }

    public async ValueTask<Comment> CreateAsync(Comment model)
    {
        _collection.InsertOne(model);

        return await GetByIdAsync(model.Id);
    }

    public ValueTask DeleteAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<Comment>.Filter.Eq(comment => ObjectId.Parse(comment.Id), objectId);

        _collection.Find(filter);

        return ValueTask.CompletedTask;
    }

    public ValueTask<IEnumerable<Comment>> GetAllAsync()
    {
        return ValueTask.FromResult(_collection.Find(Builders<Comment>.Filter.Empty).ToEnumerable());
    }

    public async ValueTask<Comment> GetByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<Comment>.Filter.Eq(comment => ObjectId.Parse(comment.Id), objectId);
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async ValueTask<Comment> UpdateAsync(Comment model)
    {
        var objectId = new ObjectId(model.Id);
        var filter = Builders<Comment>.Filter.Eq(comment => ObjectId.Parse(comment.Id), objectId);
        _collection.ReplaceOne(filter, model);

        return await GetByIdAsync(model.Id);
    }

    private static string? GetCollectionName(Type documentType)
    {
        return (documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault() as BsonCollectionAttribute)?.CollectionName;
    }
}
