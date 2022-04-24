using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Backend.Services;

public class PlaceService{
        private readonly IMongoCollection<Place> _placesCollection;
        
    public PlaceService(MongoDbService mongoDbService) {

        _placesCollection = mongoDbService.PlacesCollection;
    }
    public async Task<List<Place>> GetAllPlaceService() { 
        return await _placesCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<Place>> GetPlacesByAdminService(string adminId) { 
        return await _placesCollection.Find(x=>x.adminId==adminId).ToListAsync();
        
    }
    public async Task<Place?> GetOnePlaceService(string PlaceId){
        return await _placesCollection.Find(x=>x.Id==PlaceId).FirstOrDefaultAsync();;
    }
    public async Task CreateOnePlaceService(Place place) { 
        await _placesCollection.InsertOneAsync(place);
        return;
    }
    public async Task UpdateOnePlaceService(string placeId, Place updatedPlace) {
        await _placesCollection.ReplaceOneAsync(x => x.Id == placeId, updatedPlace);
        return;
    }
    public async Task DeleteOnePlaceService(string placeId) { 
        await _placesCollection.DeleteOneAsync(x => x.Id == placeId);
        return;
    }
        public bool placeIsCreated(string placeId){
         bool exists = _placesCollection.Find(_ => _.Id== placeId).Any();
        return exists;
    }

}