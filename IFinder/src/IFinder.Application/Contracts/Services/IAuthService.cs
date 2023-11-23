using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;

namespace IFinder.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<Response<LoginUserDto>> AuthenticateAsync(LoginUserRequest request);
        Task<Response<RegisterUserDto>> RegisterAsync(RegisterUserRequest request);
    }
}