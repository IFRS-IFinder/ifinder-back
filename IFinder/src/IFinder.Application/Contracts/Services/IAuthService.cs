using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;

namespace IFinder.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<LoginUserResponse> AuthenticateAsync(LoginUserRequest request);
    }
}