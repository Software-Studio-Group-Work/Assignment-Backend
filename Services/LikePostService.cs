using Backend.Models;
using MongoDB.Driver;


namespace Backend.Services;

public class LikePostService{
        private readonly IMongoCollection<LikePost> _likePostsCollection;
        
    public LikePostService(MongoDbService mongoDbService) {

        _likePostsCollection = mongoDbService.LikePostsCollection;
    }
    public async Task<List<LikePost>> GetAllLikePostService() { 
        return await _likePostsCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<LikePost>> GetLikesOnPostService(string postId ){
        return await _likePostsCollection.Find(x=>x.postId==postId).ToListAsync();
    }
    public async Task<LikePost?> GetOneLikePostService(string likePostId){
        return await _likePostsCollection.Find(x=>x.Id==likePostId).FirstOrDefaultAsync();
    }
    public async Task CreateOneLikePostService(LikePost likePost) { 
        await _likePostsCollection.InsertOneAsync(likePost);
        return;
    }

    public async Task DeleteOneLikePostService(string likePostId) { 
        await _likePostsCollection.DeleteOneAsync(x=>x.Id==likePostId);
        return;
    }
        public async Task DeleteLikePostByUserService(string userId) { 
        FilterDefinition<LikePost> filter= Builders<LikePost>.Filter.Eq("userId", userId);
        await _likePostsCollection.DeleteManyAsync(filter);
        return;
    }
    public bool likePostIsCreated(string likePostId){
         bool exists = _likePostsCollection.Find(_ => _.Id== likePostId).Any();
        return exists;
    }

    public bool likePostIsCreated(string userId,string postId){
         bool exists = _likePostsCollection.Find(_ => _.userId== userId&&_.postId== postId).Any();
        return exists;
    }

}