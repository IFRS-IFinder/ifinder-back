using IFinder.Domain.Contracts.Page;
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

    public async Task<Page<Card>> GetAllByUserIdAsync(string idUser, Pageable pageable)
    {
        var filter = Builders<Card>.Filter.Eq("IdUser", idUser);
        var cards = await _collection.Find(filter)
            .Skip(pageable.Offset())
            .Limit(pageable.Take)
            .ToListAsync();
        
        var totalRegisters = (int) await _collection.CountDocumentsAsync(filter);
        var totalPages = (int)Math.Ceiling((double)totalRegisters / pageable.Take);
        var isLastPage = pageable.Page == totalPages;

        return new Page<Card>(cards, totalPages, totalRegisters, isLastPage);
    }

    public async Task<Page<Card>> GetHomeAsync(string idUser, Pageable pageable)
    {
        var pipeline = new BsonDocument[]
        {
            new BsonDocument("$match", new BsonDocument("IdUser", new BsonDocument("$ne", idUser))),
            new BsonDocument("$skip", pageable.Offset()),
            new BsonDocument("$limit", pageable.Take)
        };

        var options = new AggregateOptions { AllowDiskUse = false };

        var randomCardsCursor = await _collection.AggregateAsync<Card>(pipeline, options);
        var randomCards = await randomCardsCursor.ToListAsync();

        var totalRegisters = (int) await _collection
            .CountDocumentsAsync(new BsonDocument("IdUser", new BsonDocument("$ne", idUser)));
        
        var totalPages = (int)Math.Ceiling((double)totalRegisters / pageable.Take);
        var isLastPage = pageable.Page == totalPages;

        return new Page<Card>(randomCards, totalPages, totalRegisters, isLastPage);
    }
}