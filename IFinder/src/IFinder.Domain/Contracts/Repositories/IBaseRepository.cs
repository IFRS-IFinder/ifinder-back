
namespace IFinder.Domain.Contracts.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task InsertAsync(T entity);
        Task<T> GetByIdAsync(string id);
    }
}
