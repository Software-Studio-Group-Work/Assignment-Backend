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
    public async Task<List<Announcement>> GetAdminAnnouncementService(string adminId) { 
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
        public bool AnnouncementIsCreated(string announcementId){
        var announcement = GetOneAnnouncementService(announcementId);

        if(announcement is null){

            return false;
        }
        return true;
    }

}