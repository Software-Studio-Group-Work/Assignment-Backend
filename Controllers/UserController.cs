using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _usersService;

    public UserController(UserService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<List<User>> Get() => await _usersService.GetAsync();

}