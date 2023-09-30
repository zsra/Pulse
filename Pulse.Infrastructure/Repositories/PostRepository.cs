using MongoDB.Bson;
using MongoDB.Driver;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Models;
using Pulse.Core.Settings;
using Pulse.Infrastructure.Attributes;

namespace Pulse.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IMongoCollection<Post> _collection;

    public PostRepository(IMongoSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<Post>(nameof(Post));
    }

    public async ValueTask<Post> CreateAsync(Post model)
    {
        _collection.InsertOne(model);

        return await GetByIdAsync(model.Id);
    }

    public async ValueTask DeleteAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<Post>.Filter.Eq(post => ObjectId.Parse(post.Id), objectId);

        await _collection.FindAsync(filter);
    }

    public async ValueTask<IEnumerable<Post>> GetAllAsync()
    {
        IAsyncCursor<Post> cursor = await _collection.FindAsync(Builders<Post>.Filter.Empty);
        return cursor.ToEnumerable();
    }

    public async ValueTask<Post> GetByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<Post>.Filter.Eq(post => ObjectId.Parse(post.Id), objectId);
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async ValueTask<Post> UpdateAsync(Post model)
    {
        var objectId = new ObjectId(model.Id);
        var filter = Builders<Post>.Filter.Eq(post => ObjectId.Parse(post.Id), objectId);
        _collection.ReplaceOne(filter, model);

        return await GetByIdAsync(model.Id);
    }
}
