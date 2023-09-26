using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IFinder.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService
        )
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginUserResponse>> Authenticate([FromBody] LoginUserRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);

            return Ok(response);
        }
    }
}