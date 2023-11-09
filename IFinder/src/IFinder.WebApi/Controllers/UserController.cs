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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User newUser)
    {
        await _userService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), newUser);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> Edit([FromRoute] string idUser, [FromBody] User newUser)
    {
        await _userService.EditAsync(idUser, newUser);

        return CreatedAtAction(nameof(Get), newUser);
    }
}