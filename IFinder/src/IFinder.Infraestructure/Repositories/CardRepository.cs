using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using MongoDB.Driver;

namespace IFinder.Infraestructure.Repositories;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(IMongoDatabase database) : base(database)
    {
    }
    
}