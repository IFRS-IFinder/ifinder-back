using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IFinder.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) => 
        _userService = userService;

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<GetCompleteUserDto>>> Get([FromRoute] string id)
    {
        var response = await _userService.GetUserComplete(id);
            
        if (response.IsErrorStatusCode())
            return StatusCode(response.GetErrorCode(), response.GetErrorMessage());
        
        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Response<EditUserDto>>> Edit([FromBody] EditUserRequest user)
    {
        var response = await _userService.EditAsync(user);
        
        if (response.IsErrorStatusCode())
            return StatusCode(response.GetErrorCode(), response.GetErrorMessage());
        
        return Ok(response);
    }
}