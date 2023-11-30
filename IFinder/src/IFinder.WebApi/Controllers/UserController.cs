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
    public async Task<List<User>> Get() =>
        await _userService.GetAllAsync();
    
    [HttpPatch]
    public async Task<ActionResult<Response<EditUserDto>>> Edit([FromBody] EditUserRequest user)
    {
        var editUser = await _userService.EditAsync(user);

        return CreatedAtAction(nameof(Get), editUser);
    }
}