using IFinder.Core;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IFinder.Infraestructure.Repositories;
public class UserRespository : BaseRepository<User>, IUserRepository
{
    public UserRespository(IMongoDatabase database) : base(database) { }

    public override Task InsertAsync(User entity)
    {
        entity.Password = PasswordHasher.HashPassword(entity.Password);
        return base.InsertAsync(entity);
    }

    public async Task<User?> GetUserAuthenticateAsync(string email, string password)
    {
        // var user = await _collection.FindAsync(u => u.Email == email.ToLower());
        //
        //

        var filter = Builders<User>.Filter.Eq("Email", email);

        var user = await _collection.Find(filter).FirstOrDefaultAsync();

        if (user is not null && PasswordHasher.VerifyPassword(password, user.Password))
            return user;

        return null;
    }

    public async Task<User?> EditUserAsync(string Id, User editUser)
    {
        var filter = Builders<User>.Filter.Eq("Id", Id);
        var update = Builders<User>.Update.Set(user => user, editUser);
        //var user = await _collection.Find(filter).FirstOrDefaultAsync();
        
        var user = await _collection.UpdateOneAsync(filter, update);

        if (user.IsAcknowledged && user.MatchedCount > 0)
        {
            return editUser;
        }


        return null;

    }

    public Task<bool> ExistsByEmail(string email)
    {
        throw new NotImplementedException();
    }
}
