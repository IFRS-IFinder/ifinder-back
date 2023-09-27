using IFinder.Core;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using MongoDB.Driver;

namespace IFinder.Infraestructure.Repositories;
public class UserRespository : BaseRepository<User>, IUserRepository
{
    public UserRespository(IMongoDatabase database) : base(database) {}

    public override Task InsertAsync(User entity)
    {
        entity.Password.HashPassword();
        return base.InsertAsync(entity);
    }
}
