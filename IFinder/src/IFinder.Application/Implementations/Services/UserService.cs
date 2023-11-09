using IFinder.Application.Contracts.Services;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;

namespace IFinder.Application.Implementations.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync() 
        => await _userRepository.GetAllAsync();

    public async Task CreateAsync(User newUser) 
        => await _userRepository.InsertAsync(newUser);

    public async Task EditAsync(string idUser, User newUser)
        => await _userRepository.EditAsync(idUser, newUser);
    
}