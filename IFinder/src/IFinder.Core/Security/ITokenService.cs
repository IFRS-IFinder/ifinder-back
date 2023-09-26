using IFinder.Application.Contracts.Documents.Responses;

namespace IFinder.Core.Security
{
    public interface ITokenService
    {
        string GenerateToken(LoginUserResponse user);
    }
}