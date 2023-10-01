using MongoDB.Bson;
using MongoDB.Driver;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Models;
using Pulse.Core.Settings;

namespace Pulse.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IMongoCollection<Comment> _collection;

    public CommentRepository(IMongoSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<Comment>(nameof(Comment));
    }

    public async ValueTask<Comment> CreateAsync(Comment model)
    {
        _collection.InsertOne(model);

        return await GetByIdAsync(model.Id);
    }

    public async ValueTask DeleteAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<Comment>.Filter.Eq(comment => ObjectId.Parse(comment.Id), objectId);

        await _collection.FindAsync(filter);
    }

    public async ValueTask<IEnumerable<Comment>> GetAllAsync()
    {
        IAsyncCursor<Comment> cursor = await _collection.FindAsync(Builders<Comment>.Filter.Empty);
        return cursor.ToEnumerable();
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
}
