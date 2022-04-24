using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class AnnouncementController: ControllerBase {
        private readonly UserService _userService;
    private readonly AnnouncementService _announcementService;
    public AnnouncementController(AnnouncementService announcementService,UserService userService) {
        _announcementService = announcementService;
        _userService=userService;

    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Announcement>> GetAllAnnouncement() {
        return await _announcementService.GetAllAnnouncementService();
    }
    [AllowAnonymous]
    [HttpGet("{adminId}")]
    public async Task<List<Announcement>> GetAnnouncementsByAdmin(string adminId) {

        return await _announcementService.GetAnnouncementsByAdminService(adminId);
    }
    [AllowAnonymous]
    [HttpGet("{announcementId}")]
    public async Task<ActionResult<Announcement?>> GetOneAnnouncement(string announcementId) {
                if(!_announcementService.announcementIsCreated(announcementId)){
            return NotFound();
        }

        return await _announcementService.GetOneAnnouncementService(announcementId);

    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOneAnnouncement([FromBody] Announcement newAnnouncement) {
        if(!_userService.userIdExists(newAnnouncement.adminId)){
            return NotFound("Can't create the announcement.The admin doesn't exist.");
        }
        await _announcementService.CreateOneAnnouncementService(newAnnouncement);
        return CreatedAtAction(nameof(GetOneAnnouncement), new { announcementId = newAnnouncement.Id }, newAnnouncement);
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{announcementId}")]
    public async Task<IActionResult> UpdateOneAnnouncement(string announcementId, [FromBody] Announcement updatedAnnouncement) {

        if(!_announcementService.announcementIsCreated(announcementId)){
            return NotFound();
        }

        var Announcement =await _announcementService.GetOneAnnouncementService(announcementId);

        if(Announcement!=null){
        updatedAnnouncement.Id=Announcement.Id;
        await _announcementService.UpdateOneAnnouncementService(announcementId,updatedAnnouncement);
        return Ok("Updated the Announcement");
        }else{
            return NotFound();
        }
    }
    [Authorize(Roles = "admin")]
    [HttpDelete("{announcementId}")]
    public async Task<IActionResult> DeleteOneAnnouncement(string announcementId) {

        if(!_announcementService.announcementIsCreated(announcementId)){
            return NotFound();
        }

        await _announcementService.DeleteOneAnnouncementService(announcementId);
        return Ok("Deleted the Announcement");

    }
}