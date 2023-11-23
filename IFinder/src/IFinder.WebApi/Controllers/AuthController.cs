using IFinder.Application.Contracts.Documents.Dtos;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Response<LoginUserDto>>> Authenticate([FromBody] LoginUserRequest request)
        {
            // TODO: validacao dos dados da request 

            var response = await _authService.AuthenticateAsync(request);

            if (response.IsErrorStatusCode())
                return StatusCode(response.GetErrorCode(), response.GetErrorMessage());

            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<LoginUserDto>>> Register([FromBody] RegisterUserRequest request)
        {
            // TODO: validacao dos dados da request 

            var response = await _authService.RegisterAsync(request);

            if (response.IsErrorStatusCode())
                return StatusCode(response.GetErrorCode(), response.GetErrorMessage());

            return Ok(response);
        }
    }
}