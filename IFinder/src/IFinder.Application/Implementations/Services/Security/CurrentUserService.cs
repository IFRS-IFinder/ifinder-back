
using IFootball.Application.Contracts.Services.Core;
using Microsoft.AspNetCore.Http;

namespace IFinder.Application.Implementations.Services.Security
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAcessor;
        public CurrentUserService(IHttpContextAccessor httpcontextAccessor)
        {
            _httpContextAcessor = httpcontextAccessor;
        }
        public string GetCurrentUserId()
        {
            return _httpContextAcessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
        }
    }
}
