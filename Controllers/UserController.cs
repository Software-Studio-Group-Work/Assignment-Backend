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
    private readonly LikePostService _likePostService;
    private readonly PlaceService _placeService;
    private readonly PostService _postService;
    private readonly UserService _userService;
    private readonly LikeCommentService _likeCommentService;
    private readonly CommentService _commentService;
    private readonly AnnouncementService _announcementService;
    public UserController(UserService userService,LikePostService likePostService,PlaceService placeService,PostService postService,LikeCommentService likeCommentService,AnnouncementService announcementService,CommentService commentService) {
        _userService = userService;
        _likePostService=likePostService;
        _placeService=placeService;
        _postService=postService;
        _likeCommentService=likeCommentService;
        _announcementService=announcementService;
        _commentService=commentService;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<User>> GetAllUser() {
        return await _userService.GetAllUserService();
    }
    [AllowAnonymous]
    [HttpGet("{religion}")]
    public async Task<List<User>> GetUserByReligion(string religion) {
        if(religion.Equals("all")){
            return await _userService.GetAllUserService();
        }
        return await _userService.GetUserByReligionService(religion);
    }
    [AllowAnonymous]
    [HttpGet("{token}")]
    public async Task<ActionResult<User?>> GetUserByToken(string token) {
        var user=await _userService.GetUserByTokenService(token);
        if(user==null){
        return Unauthorized("The token is invalid.");
        }
        return user;

    }
    [AllowAnonymous]
    [HttpGet("{userId}")]
    public async Task<ActionResult<User?>> GetOneUser(string userId) {


        if(!_userService.userIdExists(userId)){
            return NotFound("The user doesn't exist.");
        }

        return await _userService.GetOneUserService(userId);

    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] User anonymous) {
        if(_userService.usernameExists(anonymous.username)){
            return BadRequest("The username already exists");
        }
        await _userService.CreateOneUserService(anonymous);
        return CreatedAtAction(nameof(GetOneUser), new { userId = anonymous.Id }, anonymous);
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody]Login userLogin){
        var token = _userService.AuthenticationService(userLogin.username,userLogin.password);
        if(token=="")
            return Unauthorized("The username or password is incorrect");

        var user =_userService.GetOneUserService(userLogin.username,userLogin.password);

        return Ok(new{token,user});
    }


    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateOneUser(string userId, [FromBody] User updatedUser) {
        if(!_userService.userIdExists(userId)){
            return NotFound("The user doesn't exist.");
        }
        var user =await _userService.GetOneUserService(userId);

        if(user!=null){
        updatedUser.Id=user.Id;
        await _userService.UpdateOneUserService(userId,updatedUser);
        return Ok("Updated the user");
        }else{
        return NotFound();
        }
        
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteOneUser(string userId) {
        if(!_userService.userIdExists(userId)){
            return NotFound("The user doesn't exist.");
        }
        
        await _announcementService.DeleteAnnouncementByUserService(userId);
        await _commentService.DeleteCommentByUserService(userId);
        await _likeCommentService.DeleteLikeCommentByUserService(userId);
        await _likePostService.DeleteLikePostByUserService(userId);
        await _placeService.DeletePlaceByAdminService(userId);
        await _postService.DeletePostByUserService(userId);
        await _userService.DeleteOneUserService(userId);
        return Ok("Deleted the user");

    }

}
