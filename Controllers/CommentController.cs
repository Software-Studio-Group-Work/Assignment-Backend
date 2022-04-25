using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController: ControllerBase {
    private readonly CommentService _commentService;
    private readonly UserService _userService;
    private readonly PostService _postService;
    private readonly LikeCommentService _likeCommentService;    
    public CommentController(CommentService commentService,UserService userService,PostService postService,LikeCommentService likeCommentService) {
        _commentService = commentService;
        _postService = postService;
        _userService =userService;
        _likeCommentService = likeCommentService;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Comment>> GetAllComment() {
        return await _commentService.GetAllCommentService();
    }
    [AllowAnonymous]
    [HttpGet("{postId}")]
    public async Task<List<Comment>> GetCommentsByPost(string postId) {

        return await _commentService.GetCommentsByPostService(postId);
    }
    [AllowAnonymous]
    [HttpGet("{commentId}")]
    public async Task<ActionResult<Comment?>> GetOneComment(string commentId) {

        if(!_commentService.commentIsCreated(commentId)){
            return NotFound();
        }
        return await _commentService.GetOneCommentService(commentId);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOneComment([FromBody] Comment newComment) {
        if(!_userService.userIdExists(newComment.userId)||!_postService.postIsCreated(newComment.postId)){
             return NotFound("The post or user doesn't exist.");
        }
        await _commentService.CreateOneCommentService(newComment);
        return CreatedAtAction(nameof(GetOneComment), new { commentId = newComment.Id }, newComment);
    }
    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateOneComment(string commentId, [FromBody] Comment updatedComment) {
        if(!_commentService.commentIsCreated(commentId)){
            return NotFound();
        }
       
        var comment =await _commentService.GetOneCommentService(commentId);

        if(comment!=null){
        updatedComment.Id=comment.Id;
        await _commentService.UpdateOneCommentService(commentId,updatedComment);

        return Ok("Updated the Comment");
        }else{
            return NotFound();
        }
    }
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteOneComment(string commentId) {

        if(!_commentService.commentIsCreated(commentId)){
            return NotFound();
        }
        await _likeCommentService.DeleteLikeCommentByCommentService(commentId);
        await _commentService.DeleteOneCommentService(commentId);
        return Ok("Deleted the Comment");

    }
}