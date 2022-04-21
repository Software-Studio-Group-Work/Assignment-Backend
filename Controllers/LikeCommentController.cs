using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LikeCommentController: ControllerBase {
    private readonly LikeCommentService _likeCommentService;
    public LikeCommentController(LikeCommentService likeCommentService) {
        _likeCommentService = likeCommentService;

    }
    [HttpGet]
    public async Task<List<LikeComment>> GetAllLikeComment() {
        return await _likeCommentService.GetAllLikeCommentService();
    }

    [HttpGet("{commentId}")]
    public async Task<List<LikeComment>> GetLikesOnComment(string commentId) {
        return await _likeCommentService.GetLikesOnCommentService(commentId);

    }

    [HttpGet("{likeCommentObject}")]
    public async Task<ActionResult<LikeComment?>> GetOneLikeComment(LikeComment likeComment) {
        return await _likeCommentService.GetOneLikeCommentService(likeComment);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOneLikeComment([FromBody] LikeComment newLikeComment) {
        await _likeCommentService.CreateOneLikeCommentService(newLikeComment);
        return CreatedAtAction(nameof(GetOneLikeComment),newLikeComment);
    }

    [HttpDelete("{likeCommentObject}")]
    public async Task<IActionResult> DeleteOneLikeComment(LikeComment likeComment) {

        if(!_likeCommentService.LikeCommentIsCreated(likeComment)){
            return NotFound();
        }

        await _likeCommentService.DeleteOneLikeCommentService(likeComment);
        return Ok("Deleted the LikeComment");

    }
}