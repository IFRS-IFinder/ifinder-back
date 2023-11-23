using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Domain.Models;

namespace IFinder.Application.Implementations.Mappers;

public static class UserMapper
{
    public static User ToUserDto (this User user)
    {
        return new User
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}