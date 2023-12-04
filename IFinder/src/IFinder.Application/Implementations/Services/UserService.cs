using System.Net;
using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Dtos.User;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Application.Contracts.Services.Security;
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

    public async Task<Response<GetSimpleUserDto>> GetUserSimple(string id)
    {
        var loggedUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(id);
        var isAuthor = loggedUserId.Equals(id);
        
        if(user is null)
            return new Response<GetSimpleUserDto>(HttpStatusCode.UnprocessableEntity, "Usuário não existe!");

        return new Response<GetSimpleUserDto>(new GetSimpleUserDto()
        {
            Name = user.Name,
            Sex = user.Sex, 
            Age = user.Age, 
            Description = user.Description, 
            Hobbies = user.Hobbies, 
            isAuthor = isAuthor, 
        });

    }

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

    public async Task<Response<GetCompleteUserDto>> GetUserComplete(string id)
    {
        var loggedUserId = _currentUserService.GetCurrentUserId();
        if(loggedUserId != id)
            return new Response<GetCompleteUserDto>(HttpStatusCode.Forbidden, "Este usuário não é seu!");
        
        var user = await _userRepository.GetByIdAsync(id);
        if(user is null)
            return new Response<GetCompleteUserDto>(HttpStatusCode.UnprocessableEntity, "Usuário não existe!");

        return new Response<GetCompleteUserDto>(new GetCompleteUserDto()
        {
            Email = user.Email, 
            Sex = user.Sex, 
            Age = user.Age, 
            Description = user.Description, 
            Hobbies = user.Hobbies, 
        });

    }
}