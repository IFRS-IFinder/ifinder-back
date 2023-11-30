using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IFinder.Infraestructure.Repositories;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(IMongoDatabase database) : base(database)
    {
    }

    public async Task<IEnumerable<Card>> GetAllByUserIdAsync(string idUser)
    {
        var filter = Builders<Card>.Filter.Eq("IdUser", idUser);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Card>> GetHomeAsync(string idUser)
    {
        var pipeline = new BsonDocument[]
        {
            new BsonDocument("$match", new BsonDocument("IdUser", new BsonDocument("$ne", idUser))),
            new BsonDocument("$sample", new BsonDocument("size", 10))
        };

        var options = new AggregateOptions { AllowDiskUse = false };

        var randomCardsCursor = await _collection.AggregateAsync<Card>(pipeline, options);
        var randomCards = await randomCardsCursor.ToListAsync();

        return randomCards;
    }
}