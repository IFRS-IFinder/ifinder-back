using IFinder.Application.Contracts.Documents.Dtos;

namespace IFinder.Application.Contracts.Documents.Responses;

public class LoginUserResponse
{
    public LoginUserDto? User { get; set; }
    public string? Token { get; set; }

    public LoginUserResponse(LoginUserDto user)
    {
        User = user;
    }
}