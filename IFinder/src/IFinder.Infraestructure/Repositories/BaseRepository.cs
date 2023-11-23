using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using MongoDB.Driver;

namespace IFinder.Infraestructure.Repositories
{
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

        public virtual async Task InsertAsync(T entity) 
            => await _collection.InsertOneAsync(entity);     

    }
}

