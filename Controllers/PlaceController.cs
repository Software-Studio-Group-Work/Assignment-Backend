using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class PlaceController: ControllerBase {
    private readonly PlaceService _placeService;
    private readonly UserService _userService;
    public PlaceController(PlaceService PlaceService,UserService userService) {
        _placeService = PlaceService;
        _userService =userService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Place>> GetAllPlace() {
        return await _placeService.GetAllPlaceService();
    }
    [AllowAnonymous]
    [HttpGet("{adminId}")]
    public async Task<List<Place>> GetPlacesByAdmin(string adminId) {
        return await _placeService.GetPlacesByAdminService(adminId);
    }
    [AllowAnonymous]
    [HttpGet("{placeId}")]
    public async Task<ActionResult<Place?>> GetOnePlace(string placeId) {
        if(!_placeService.placeIsCreated(placeId)){
            return NotFound();
        }
        return await _placeService.GetOnePlaceService(placeId);

    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOnePlace([FromBody] Place newPlace) {
        if(!_userService.userIdExists(newPlace.adminId)){
            return NotFound("Can't create the place.The admin doesn't exist.");
        }
        await _placeService.CreateOnePlaceService(newPlace);
        return CreatedAtAction(nameof(GetOnePlace), new { placeId = newPlace.Id }, newPlace);
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{placeId}")]
    public async Task<IActionResult> UpdateOnePlace(string placeId, [FromBody] Place updatedPlace) {
        if(!_placeService.placeIsCreated(placeId)){
            return NotFound();
        }
        var Place =await _placeService.GetOnePlaceService(placeId);

        if(Place!=null){
        updatedPlace.Id=Place.Id;
        await _placeService.UpdateOnePlaceService(placeId,updatedPlace);
        return Ok("Updated the Place");
        }else{
            return NotFound();
        }
    }
    [Authorize(Roles = "admin")]
    [HttpDelete("{placeId}")]
    public async Task<IActionResult> DeleteOnePlace(string placeId) {

        if(!_placeService.placeIsCreated(placeId)){
            return NotFound();
        }

        await _placeService.DeleteOnePlaceService(placeId);
        return Ok("Deleted the Place");

    }
}