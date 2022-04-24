using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Backend.Services;

public class PostService{
        private readonly IMongoCollection<Post> _postCollection;
        
    public PostService(MongoDbService mongoDbService) {

        _postCollection = mongoDbService.PostsCollection;
    }
    public async Task<List<Post>> GetAllPostService() { 
        return await _postCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<Post>> GetPostsByUserService(string userId) { 
        return await _postCollection.Find(x=>x.userId==userId).ToListAsync();
        
    }
        public async Task<List<Post>> GetPostsByReligionService(string religion) { 
        return await _postCollection.Find(x=>x.religion==religion).ToListAsync();
        
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
        public bool postIsCreated(string postId){
        bool exists = _postCollection.Find(_ => _.Id== postId).Any();
        return exists;
    }

}