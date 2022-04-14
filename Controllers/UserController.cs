using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SoftStuApi.Services;
using SoftStuApi.Models;

namespace SoftStuApi.Controllers;
[Authorize]
[Controller]
[Route("api/[controller]/[action]")]
public class UserController: ControllerBase {
    
    private readonly UserService _userService;
    public UserController(UserService UserService) {
        _userService = UserService;
    }
    [HttpGet]
    public async Task<List<User>> GetAllUser() {
        return await _userService.GetAllUserService();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<User?>> GetOneUser(string userId) {

        var User =await _userService.GetOneUserService(userId);
        if(User is null){
            return NotFound();
        }

        return await _userService.GetOneUserService(userId);

    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] User newUser) {
        await _userService.CreateOneUserService(newUser);
        return CreatedAtAction(nameof(GetOneUser), new { userId = newUser.Id }, newUser);
    }



    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateOneUser(string userId, [FromBody] User updatedUser) {
        var User =await _userService.GetOneUserService(userId);
        if(User is null){
            return NotFound();
        }

        updatedUser.Id=User.Id;
        await _userService.UpdateOneUserService(userId,updatedUser);

        return NoContent();
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteOneUser(string userId) {
        var User =await _userService.GetOneUserService(userId);
        if(User is null){
            return NotFound();
        }

        await _userService.DeleteOneUserService(userId);
        return NoContent();

    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] Login user){
        var token = _userService.AuthenticationService(user.username,user.password);
        if(token==null)
            return Unauthorized();
        return Ok(new{token});
    }
}