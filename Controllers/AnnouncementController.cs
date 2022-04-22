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
    private readonly AnnouncementService _announcementService;
    public AnnouncementController(AnnouncementService announcementService) {
        _announcementService = announcementService;

    }

    [AllowAnonymous]
    public async Task<List<Announcement>> GetAllAnnouncement() {
        return await _announcementService.GetAllAnnouncementService();
    }
    [AllowAnonymous]
    [HttpGet("{AdminId}")]
    public async Task<List<Announcement>> GetAdminAnnouncement(string AdminId) {

        return await _announcementService.GetAdminAnnouncementService(AdminId);
    }
    [AllowAnonymous]
    [HttpGet("{AnnouncementId}")]
    public async Task<ActionResult<Announcement?>> GetOneAnnouncement(string AnnouncementId) {
        return await _announcementService.GetOneAnnouncementService(AnnouncementId);

    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOneAnnouncement([FromBody] Announcement newAnnouncement) {
        await _announcementService.CreateOneAnnouncementService(newAnnouncement);
        return CreatedAtAction(nameof(GetOneAnnouncement), new { AnnouncementId = newAnnouncement.Id }, newAnnouncement);
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{AnnouncementId}")]
    public async Task<IActionResult> UpdateOneAnnouncement(string AnnouncementId, [FromBody] Announcement updatedAnnouncement) {
        if(!_announcementService.AnnouncementIsCreated(AnnouncementId)){
            return NotFound();
        }
        var Announcement =await _announcementService.GetOneAnnouncementService(AnnouncementId);
        updatedAnnouncement.Id=Announcement.Id;
        await _announcementService.UpdateOneAnnouncementService(AnnouncementId,updatedAnnouncement);

        return Ok("Updated the Announcement");
    }
    [Authorize(Roles = "admin")]
    [HttpDelete("{AnnouncementId}")]
    public async Task<IActionResult> DeleteOneAnnouncement(string AnnouncementId) {

        if(!_announcementService.AnnouncementIsCreated(AnnouncementId)){
            return NotFound();
        }

        await _announcementService.DeleteOneAnnouncementService(AnnouncementId);
        return Ok("Deleted the Announcement");

    }
}