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
    public PlaceController(PlaceService PlaceService) {
        _placeService = PlaceService;

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
        return await _placeService.GetOnePlaceService(placeId);

    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOnePlace([FromBody] Place newPlace) {
        await _placeService.CreateOnePlaceService(newPlace);
        return CreatedAtAction(nameof(GetOnePlace), new { placeId = newPlace.Id }, newPlace);
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{placeId}")]
    public async Task<IActionResult> UpdateOnePlace(string placeId, [FromBody] Place updatedPlace) {

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

        if(!_placeService.PlaceIsCreated(placeId)){
            return NotFound();
        }

        await _placeService.DeleteOnePlaceService(placeId);
        return Ok("Deleted the Place");

    }
}