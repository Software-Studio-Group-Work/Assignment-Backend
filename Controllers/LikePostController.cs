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
    public LikePostController(LikePostService likePostService) {
        _likePostService = likePostService;

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
        return await _likePostService.GetOneLikePostService(likePostId);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOneLikePost([FromBody] LikePost newLikePost) {
        await _likePostService.CreateOneLikePostService(newLikePost);
        return CreatedAtAction(nameof(GetOneLikePost),new {likePostId=newLikePost.Id},newLikePost);
    }

    [HttpDelete("{likePostId}")]
    public async Task<IActionResult> DeleteOneLikePost(string likePostId) {

        if(!_likePostService.LikePostIsCreated(likePostId)){
            return NotFound();
        }

        await _likePostService.DeleteOneLikePostService(likePostId);
        return Ok("Deleted the LikePost");

    }
}