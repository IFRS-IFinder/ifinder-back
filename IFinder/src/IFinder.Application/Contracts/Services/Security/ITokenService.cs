using IFinder.Application.Contracts.Documents.Dtos;

namespace IFinder.Application.Contracts.Services.Security;
public interface ITokenService
{
    string GenerateToken(LoginUserDto user);
}