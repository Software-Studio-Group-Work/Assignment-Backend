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
    public CommentController(CommentService commentService) {
        _commentService = commentService;

    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Comment>> GetAllComment() {
        return await _commentService.GetAllCommentService();
    }
    [AllowAnonymous]
    [HttpGet("{postId}")]
    public async Task<List<Comment>> GetPostComment(string postId) {

        return await _commentService.GetPostCommentService(postId);
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
        await _commentService.CreateOneCommentService(newComment);
        return CreatedAtAction(nameof(GetOneComment), new { commentId = newComment.Id }, newComment);
    }
    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateOneComment(string commentId, [FromBody] Comment updatedComment) {
       

        if(!_commentService.commentIsCreated(commentId)){
            return NotFound();
        }
        var comment =await _commentService.GetOneCommentService(commentId);
        updatedComment.Id=comment.Id;
        await _commentService.UpdateOneCommentService(commentId,updatedComment);

        return Ok("Updated the Comment");
    }
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteOneComment(string commentId) {

        if(!_commentService.commentIsCreated(commentId)){
            return NotFound();
        }

        await _commentService.DeleteOneCommentService(commentId);
        return Ok("Deleted the Comment");

    }
}