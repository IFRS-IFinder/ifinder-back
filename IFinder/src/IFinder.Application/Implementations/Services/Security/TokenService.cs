using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Services.Security;
using IFinder.Core.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IFinder.Application.Implementations.Services.Security
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(LoginUserDto user)
        {
            var token_handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSettings.SecretKey);
            var token_descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(TokenSettings.ExpiresInHours),
                SigningCredentials = new SigningCredentials( 
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };
            var token = token_handler.CreateToken(token_descriptor);

            return token_handler.WriteToken(token);
        }
    }
}