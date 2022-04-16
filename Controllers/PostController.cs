using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;

namespace SoftStuApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PostController: ControllerBase {
    
    private readonly PostService _postService;
    private readonly UserService _userService;
    public PostController(PostService postService,UserService userService) {
        _postService = postService;
        _userService =userService;
    }
    [HttpGet]
    public async Task<List<Post>> GetAllPost() {
        return await _postService.GetAllPostService();
    }
    [HttpGet("{userId}")]
    public async Task<List<Post>> GetUserPost(string userId) {

        return await _postService.GetUserPostService(userId);
    }
    [HttpGet("{postId}")]
    public async Task<ActionResult<Post?>> GetOnePost(string postId) {

        var post =await _postService.GetOnePostService(postId);
        if(post is null){
            return NotFound();
        }

        return await _postService.GetOnePostService(postId);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOnePost([FromBody] Post newPost) {

        if(!_userService.userIsCreated(newPost.userId)){
            return NotFound();
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
        updatedPost.Id=post.Id;
        await _postService.UpdateOnePostService(postId,updatedPost);

        return Ok("Updated the post");
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