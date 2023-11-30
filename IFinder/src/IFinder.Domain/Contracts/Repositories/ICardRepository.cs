using IFinder.Domain.Models;

namespace IFinder.Domain.Contracts.Repositories;

public interface ICardRepository : IBaseRepository<Card>
{
    Task<IEnumerable<Card>> GetAllByUserIdAsync(string idUser);

    Task<IEnumerable<Card>> GetHomeAsync(string idUser);
}