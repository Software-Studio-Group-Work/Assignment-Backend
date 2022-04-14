using SoftStuApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SoftStuApi.Services;

public class PostService{
        private readonly IMongoCollection<Post> _postCollection;
        
    public PostService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _postCollection = database.GetCollection<Post>(mongoDBSettings.Value.CollectionName);
    }
    public async Task<List<Post>> GetAllPostService() { 
        return await _postCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<Post>> GetUserPostService(string userId) { 
        return await _postCollection.Find(x=>x.Id==userId).ToListAsync();
        
    }
    public async Task<Post?> GetOnePostService(string postId){
        return await _postCollection.Find(x=>x.Id==postId).FirstOrDefaultAsync();
    }
    public async Task CreateOnePostService(Post post) { 
        await _postCollection.InsertOneAsync(post);
        return;
    }
    public async Task UpdateOnePostService(string postId, Post updatedPost) {
        await _postCollection.ReplaceOneAsync(x => x.Id == postId, updatedPost);
        return;
    }
    public async Task DeleteOnePostService(string postId) { 
        await _postCollection.DeleteOneAsync(x => x.Id == postId);
        return;
    }

}