using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Application.Implementations.Mappers;
using IFinder.Domain.Models;
using MongoDB.Driver;

namespace IFinder.Application.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMongoCollection<User> _userCollection;
        
        public AuthService(
            IMongoDatabase database
        )
        {
            _userCollection = database.GetCollection<User>("AppUsers");
        }

        public async Task<LoginUserResponse> AuthenticateAsync(LoginUserRequest request)
        {
            // tests //
            var user = await _userCollection.FindAsync(u => u.Email.ToLower() == request.Email.ToLower());

            if (user is null) return null;

            return new LoginUserResponse(
                user
                    .FirstOrDefault()
                    .ToLoginUserDto()
            );
        }
    }
}