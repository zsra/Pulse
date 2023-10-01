using MongoDB.Bson;
using MongoDB.Driver;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Models;
using Pulse.Core.Settings;

namespace Pulse.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<User>(nameof(User));
    }

    public async ValueTask<User> CreateAsync(User model)
    {
        _collection.InsertOne(model);

        return await GetByIdAsync(model.Id);
    }

    public async ValueTask DeleteAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<User>.Filter.Eq(user => ObjectId.Parse(user.Id), objectId);

        await _collection.FindAsync(filter);
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync()
    {
        IAsyncCursor<User> cursor = await _collection.FindAsync(Builders<User>.Filter.Empty);
        return cursor.ToEnumerable();
    }

    public async ValueTask<User> GetByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<User>.Filter.Eq(user => ObjectId.Parse(user.Id), objectId);
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async ValueTask<User> UpdateAsync(User model)
    {
        var objectId = new ObjectId(model.Id);
        var filter = Builders<User>.Filter.Eq(user => ObjectId.Parse(user.Id), objectId);
        _collection.ReplaceOne(filter, model);

        return await GetByIdAsync(model.Id);
    }

    public async ValueTask<User> GetUserByEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Email, email);
        IAsyncCursor<User> cursor = await _collection.FindAsync(filter);

        return cursor.Current.FirstOrDefault()!;
    }
}

