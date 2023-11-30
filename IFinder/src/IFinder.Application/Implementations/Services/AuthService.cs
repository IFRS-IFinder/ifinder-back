using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Application.Contracts.Services.Security;
using IFinder.Application.Implementations.Mappers;
using IFinder.Domain.Contracts.Repositories;
using System.Net;
using IFinder.Domain.Models;

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

        public async Task<Response<RegisterUserDto>> RegisterAsync(RegisterUserRequest request)
        {
            var userEmailExists = await _userRepository.UserExistsByEmail(request.Email);

            if (userEmailExists)
                return new Response<RegisterUserDto>(HttpStatusCode.UnprocessableEntity, "Email j� existe!");

            var name = new Random();
            
            var user = new User()
            {
                Name = name.Next().ToString(),
                Email = request.Email,
                Password = request.Password,
                Sex = request.Sex,
                Age = request.Age,
                Description = request.Description,
                Hobbies = request.Hobbies,
            };
            await _userRepository.InsertAsync(user);

            
            return new Response<RegisterUserDto>(new RegisterUserDto
            {
                Id = user.Id,
                Name = user.Name
            });
        }
    }
}