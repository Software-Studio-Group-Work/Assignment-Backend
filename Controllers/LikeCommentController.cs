using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class LikeCommentController: ControllerBase {
    private readonly LikeCommentService _likeCommentService;
    private readonly UserService _userService;
    private readonly CommentService _commentService;
    public LikeCommentController(LikeCommentService likeCommentService,UserService userService,CommentService commentService) {
        _likeCommentService = likeCommentService;
        _userService=userService;
        _commentService=commentService;

    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<LikeComment>> GetAllLikeComment() {
        return await _likeCommentService.GetAllLikeCommentService();
    }
    [AllowAnonymous]
    [HttpGet("{commentId}")]
    public async Task<List<LikeComment>> GetLikesOnComment(string commentId) {
        return await _likeCommentService.GetLikesOnCommentService(commentId);

    }
    [AllowAnonymous]
    [HttpGet("{likeCommentId}")]
    public async Task<ActionResult<LikeComment?>> GetOneLikeComment(string likeCommentId) {
        if(!_likeCommentService.likeCommentIsCreated(likeCommentId)){
            return NotFound();
        }
        return await _likeCommentService.GetOneLikeCommentService(likeCommentId);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOneLikeComment([FromBody] LikeComment newLikeComment) {
               if(!_userService.userIdExists(newLikeComment.userId)||!_commentService.commentIsCreated(newLikeComment.commentId)){
             return NotFound("The comment or user doesn't exist.");
        }
        await _likeCommentService.CreateOneLikeCommentService(newLikeComment);
        return CreatedAtAction(nameof(GetOneLikeComment),new {likeCommentId=newLikeComment.Id},newLikeComment);
    }

    [HttpDelete("{likeCommentId}")]
    public async Task<IActionResult> DeleteOneLikeComment(string likeCommentId) {

        if(!_likeCommentService.likeCommentIsCreated(likeCommentId)){
            return NotFound();
        }

        await _likeCommentService.DeleteOneLikeCommentService(likeCommentId);
        return Ok("Deleted the LikeComment");

    }
}