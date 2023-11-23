using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Application.Contracts.Services.Security;
using IFinder.Application.Implementations.Mappers;
using IFinder.Domain.Contracts.Repositories;
using System.Net;

namespace IFinder.Application.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Response<LoginUserDto>> AuthenticateAsync(LoginUserRequest request)
        {

            var user = await _userRepository.GetUserAuthenticateAsync(request.Email, request.Password);

            if (user is null)
                return new Response<LoginUserDto>(HttpStatusCode.Unauthorized, "Email ou senha incorretos!");

            var userDto = user.ToLoginUserDto();
            userDto.Token = _tokenService.GenerateToken(userDto);

            return new Response<LoginUserDto>(userDto);
        }
    }
}