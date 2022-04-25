using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class PostController: ControllerBase {
    
    private readonly PostService _postService;
    private readonly UserService _userService;
    public PostController(PostService postService,UserService userService) {
        _postService = postService;
        _userService =userService;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Post>> GetAllPost() {
        return await _postService.GetAllPostService();
    }
    [AllowAnonymous]
    [HttpGet("{userId}")]
    public async Task<List<Post>> GetPostsByUser(string userId) {
        return await _postService.GetPostsByUserService(userId);
    }
    [AllowAnonymous]
    [HttpGet("{religion}")]
    public async Task<List<Post>> GetPostsByReligion(string religion) {
        if(religion.Equals("all")){
            return await _postService.GetAllPostService();
        }
        return await _postService.GetPostsByReligionService(religion);
    }
    [AllowAnonymous]
    [HttpGet("{postId}")]
    public async Task<ActionResult<Post?>> GetOnePost(string postId) {

        if(!_postService.postIsCreated(postId)){
            return NotFound();
        }

        return await _postService.GetOnePostService(postId);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOnePost([FromBody] Post newPost) {

        if(!_userService.userIdExists(newPost.userId)){
            return NotFound("Can't create the post.The user doesn't exist.");
        }
        await _postService.CreateOnePostService(newPost);
        return CreatedAtAction(nameof(GetOnePost), new { postId = newPost.Id }, newPost);
    }
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdateOnePost(string postId, [FromBody] Post updatedPost) {
        if(!_postService.postIsCreated(postId)){
            return NotFound();
        }
        var post =await _postService.GetOnePostService(postId);
        if(post!=null){
        updatedPost.Id=post.Id;
        await _postService.UpdateOnePostService(postId,updatedPost);

        return Ok("Updated the post");
        }else{
             return NotFound();
        }
    }
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeleteOnePost(string postId) {

        if(!_postService.postIsCreated(postId)){
            return NotFound();
        }

        await _postService.DeleteOnePostService(postId);
        return Ok("Deleted the post");

    }
}