using IFinder.Domain.Models;

namespace IFinder.Domain.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserAuthenticateAsync(string email, string password);
        Task<bool> UserExistsByEmail(string email);
        Task<User?> EditUserAsync(string Id, User editUser);
    }
}
