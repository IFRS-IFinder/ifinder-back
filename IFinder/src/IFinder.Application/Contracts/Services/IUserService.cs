using IFinder.Domain.Models;

namespace IFinder.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task CreateAsync(User newUser);
    }
}