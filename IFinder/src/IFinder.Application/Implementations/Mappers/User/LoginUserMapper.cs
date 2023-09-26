using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Domain.Models;

namespace IFinder.Application.Implementations.Mappers;

public static class LoginUserMapper
{
    public static LoginUserDto ToLoginUserDto (this User user)
    {
        return new LoginUserDto
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}