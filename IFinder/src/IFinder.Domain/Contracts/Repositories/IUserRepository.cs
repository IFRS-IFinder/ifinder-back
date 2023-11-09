using IFinder.Domain.Models;

namespace IFinder.Domain.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserAuthenticateAsync(string email, string password);

    }
}
