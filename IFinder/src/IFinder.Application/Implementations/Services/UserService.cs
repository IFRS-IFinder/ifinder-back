using System.Net;
using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using IFinder.Application.Contracts.Services.Security;
using System.Reflection;
using IFinder.Core;

namespace IFinder.Application.Implementations.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UserService(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<User>> GetAllAsync()
        => await _userRepository.GetAllAsync();

    public async Task<Response<EditUserDto>> EditAsync(EditUserRequest userRequest)
    {
        var idUser = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(idUser);

        if (user is null)
            return new Response<EditUserDto>(HttpStatusCode.UnprocessableEntity, "Usuário não existe!");

        //If se tem senha ou não

        foreach (PropertyInfo? prop in userRequest.GetType().GetProperties())
        {
            var propValue = prop?.GetValue(userRequest);
            if (propValue is not null)
            {
                if (prop.Name == "Password")
                {
                    propValue = PasswordHasher.HashPassword(propValue.ToString());
                }
                user.GetType().GetProperty(prop.Name).SetValue(user, propValue);
            }
        }

        await _userRepository.EditUserAsync(idUser, user);

        return new Response<EditUserDto>(new EditUserDto()
        {
            Id = user.Id,
            Name = user.Name,
        });
    }


}