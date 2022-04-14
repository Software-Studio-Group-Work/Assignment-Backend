using System;
using Microsoft.AspNetCore.Mvc;
using SoftStuApi.Services;
using SoftStuApi.Models;

namespace SoftStuApi.Controllers;

[Controller]
[Route("api/[controller]/[action]")]
public class PostController: ControllerBase {
    
    private readonly PostService _postService;
    public PostController(PostService postService) {
        _postService = postService;
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
        await _postService.CreateOnePostService(newPost);
        return CreatedAtAction(nameof(GetOnePost), new { postId = newPost.Id }, newPost);
    }
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdateOnePost(string postId, [FromBody] Post updatedPost) {
        var post =await _postService.GetOnePostService(postId);
        if(post is null){
            return NotFound();
        }

        updatedPost.Id=post.Id;
        await _postService.UpdateOnePostService(postId,updatedPost);

        return NoContent();
    }
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeleteOnePost(string postId) {
        var post =await _postService.GetOnePostService(postId);
        if(post is null){
            return NotFound();
        }

        await _postService.DeleteOnePostService(postId);
        return NoContent();

    }
}