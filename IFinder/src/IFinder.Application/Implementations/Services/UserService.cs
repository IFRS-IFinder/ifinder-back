using IFinder.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IFinder.Application.Implementations.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(
        IMongoDatabase database)
    {
        
        // IMongoDatabase mongoDb = mongoClient.GetDatabase("IFinder");
        _userCollection = database.GetCollection<User>("AppUsers");
    }

    public async Task<List<User>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();

    public async Task CreateAsync(User newUser) => await _userCollection.InsertOneAsync(newUser);
}