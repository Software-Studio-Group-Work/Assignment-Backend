using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class LikePostController: ControllerBase {
    private readonly LikePostService _likePostService;
    private readonly UserService _userService;
    private readonly PostService _postService;
    public LikePostController(LikePostService likePostService,UserService userService,PostService postService) {
        _likePostService = likePostService;
        _userService=userService;
        _postService=postService;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<LikePost>> GetAllLikePost() {
        return await _likePostService.GetAllLikePostService();
    }
    [AllowAnonymous]
    [HttpGet("{postId}")]
    public async Task<List<LikePost>> GetLikesOnPost(string postId) {
        return await _likePostService.GetLikesOnPostService(postId);

    }
    [AllowAnonymous]
    [HttpGet("{likePostId}")]
    public async Task<ActionResult<LikePost?>> GetOneLikePost(string likePostId) {
        if(!_likePostService.likePostIsCreated(likePostId)){
            return NotFound();
        }
        return await _likePostService.GetOneLikePostService(likePostId);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOneLikePost([FromBody] LikePost newLikePost) {
        if(!_userService.userIdExists(newLikePost.userId)||!_postService.postIsCreated(newLikePost.postId)){
             return NotFound("The post or user doesn't exist.");
        }
        if(_likePostService.likePostIsCreated(newLikePost.userId,newLikePost.postId)){
            return BadRequest("The user has already liked this post.");
        }
        await _likePostService.CreateOneLikePostService(newLikePost);
        return CreatedAtAction(nameof(GetOneLikePost),new {likePostId=newLikePost.Id},newLikePost);
    }

    [HttpDelete("{likePostId}")]
    public async Task<IActionResult> DeleteOneLikePost(string likePostId) {

        if(!_likePostService.likePostIsCreated(likePostId)){
            return NotFound();
        }

        await _likePostService.DeleteOneLikePostService(likePostId);
        return Ok("Deleted the LikePost");

    }
}