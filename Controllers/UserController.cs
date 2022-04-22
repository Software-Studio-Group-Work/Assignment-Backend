using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Services;
using Backend.Models;

namespace Backend.Controllers;
[Authorize]
[ApiController]
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


        if(!_userService.userIsCreated(userId)){
            return NotFound();
        }

        return await _userService.GetOneUserService(userId);

    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] User anonymous) {
        await _userService.CreateOneUserService(anonymous);
        return CreatedAtAction(nameof(GetOneUser), new { userId = anonymous.Id }, anonymous.Id);
    }



    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateOneUser(string userId, [FromBody] User updatedUser) {


        if(!_userService.userIsCreated(userId)){
            return NotFound();
        }

        var user =await _userService.GetOneUserService(userId);
        updatedUser.Id=user.Id;
        await _userService.UpdateOneUserService(userId,updatedUser);

        return Ok("Updated the user");
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteOneUser(string userId) {
        if(!_userService.userIsCreated(userId)){
            return NotFound();
        }

        await _userService.DeleteOneUserService(userId);
        return Ok("Deleted the user");

    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody]Login userLogin){
        var token = _userService.AuthenticationService(userLogin.username,userLogin.password);
        if(token==null)
            return Unauthorized();

        var user =_userService.GetOneUserService(userLogin.username,userLogin.password);

        return Ok(new{token,user});
    }
}
