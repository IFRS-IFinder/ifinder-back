using IFinder.Domain.Contracts.Page;
using IFinder.Domain.Models;

namespace IFinder.Domain.Contracts.Repositories;

public interface ICardRepository : IBaseRepository<Card>
{
    Task<Page<Card>> GetAllByUserIdAsync(string idUser, Pageable pageable);

    Task<Page<Card>> GetHomeAsync(string idUser, Pageable pageable);
}