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
    [HttpGet("{LikePostObject}")]
    public async Task<ActionResult<LikePost?>> GetOneLikePost(LikePost likePost) {
        return await _likePostService.GetOneLikePostService(likePost);

    }
    [HttpPost]
    public async Task<IActionResult> CreateOneLikePost([FromBody] LikePost newLikePost) {
        await _likePostService.CreateOneLikePostService(newLikePost);
        return CreatedAtAction(nameof(GetOneLikePost),newLikePost);
    }

    [HttpDelete("{LikePostObject}")]
    public async Task<IActionResult> DeleteOneLikePost(LikePost likePost) {

        if(!_likePostService.LikePostIsCreated(likePost)){
            return NotFound();
        }

        await _likePostService.DeleteOneLikePostService(likePost);
        return Ok("Deleted the LikePost");

    }
}