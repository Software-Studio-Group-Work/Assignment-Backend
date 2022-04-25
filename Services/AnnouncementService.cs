using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Backend.Services;

public class AnnouncementService{
        private readonly IMongoCollection<Announcement> _announcementsCollection;
        
    public AnnouncementService(MongoDbService mongoDbService) {

        _announcementsCollection = mongoDbService.AnnouncementsCollection;
    }
    public async Task<List<Announcement>> GetAllAnnouncementService() { 
        return await _announcementsCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<Announcement>> GetAnnouncementsByAdminService(string adminId) { 
        return await _announcementsCollection.Find(x=>x.adminId==adminId).ToListAsync();
        
    }
    public async Task<Announcement?> GetOneAnnouncementService(string announcementId){
        return await _announcementsCollection.Find(x=>x.Id==announcementId).FirstOrDefaultAsync();;
    }
    public async Task CreateOneAnnouncementService(Announcement announcement) { 
        await _announcementsCollection.InsertOneAsync(announcement);
        return;
    }
    public async Task UpdateOneAnnouncementService(string announcementId, Announcement updatedAnnouncement) {
        await _announcementsCollection.ReplaceOneAsync(x => x.Id == announcementId, updatedAnnouncement);
        return;
    }
    public async Task DeleteOneAnnouncementService(string announcementId) { 
        await _announcementsCollection.DeleteOneAsync(x => x.Id == announcementId);
        return;
    }
        public async Task DeleteAnnouncementByAdminService(string adminId) { 
        FilterDefinition<Announcement> filter= Builders<Announcement>.Filter.Eq("adminId", adminId);
        await _announcementsCollection.DeleteManyAsync(filter);
        return;
    }
        public bool announcementIsCreated(string announcementId){
         bool exists = _announcementsCollection.Find(_ => _.Id== announcementId).Any();
        return exists;
    }

}