using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Core;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using MongoDB.Driver;
using System.Reflection;

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

        var filter = Builders<User>.Filter.Eq("Email", email);

        var user = await _collection.Find(filter).FirstOrDefaultAsync();

        if (user is not null && PasswordHasher.VerifyPassword(password, user.Password))
            return user;

        return null;
    }

    public async Task<User?> GetUserById(string id)
    {
        var filter = Builders<User>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> EditUserAsync(string id, User editUser)
    {
        var filter = Builders<User>.Filter.Eq("Id", id);

        var user = await _collection.ReplaceOneAsync(filter, editUser);

        if (user.IsAcknowledged && user.MatchedCount > 0)
        {
            return new User { };
        }

        return null;
    }

    public async Task<bool> UserExistsByEmail(string email)
    {
        var filter = Builders<User>.Filter.Eq("Email", email);
        var user = await _collection.Find(filter).FirstOrDefaultAsync();
        return user is not null;
    }

    public static void ChangeProp<T,U>(T entity, U editEntity)
    {
        foreach (var prop in entity.GetType().GetProperties())
        {
            PropertyInfo? property = editEntity?.GetType().GetProperty(prop.Name);
            property?.SetValue(editEntity, prop.GetValue(entity));
        }
    }
}
