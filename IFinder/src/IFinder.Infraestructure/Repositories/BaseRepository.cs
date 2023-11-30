using IFinder.Domain.Contracts.Repositories;
using MongoDB.Driver;

namespace IFinder.Infraestructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class 
{
    protected readonly IMongoCollection<T> _collection;
    public BaseRepository(
        IMongoDatabase database
        )
    {
        _collection = database.GetCollection<T>($"{typeof(T).Name}s");
    }

    public virtual async Task<List<T>> GetAllAsync() 
        => await _collection.Find(_ => true).ToListAsync();

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task DeleteByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }
    


    public virtual async Task InsertAsync(T entity) 
        => await _collection.InsertOneAsync(entity);     
}