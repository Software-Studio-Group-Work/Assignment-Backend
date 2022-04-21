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
    public async Task<LikePost?> GetOneLikePostService(LikePost likePost ){
        return await _likePostsCollection.Find(x=>(x.userId==likePost.userId&&x.postId==likePost.postId)).FirstOrDefaultAsync();
    }
    public async Task CreateOneLikePostService(LikePost likePost) { 
        await _likePostsCollection.InsertOneAsync(likePost);
        return;
    }

    public async Task DeleteOneLikePostService(LikePost likePost) { 
        await _likePostsCollection.DeleteOneAsync(x =>(x.userId==likePost.userId&&x.postId==likePost.postId));
        return;
    }
        public bool LikePostIsCreated(LikePost likePost){
        var LikePost = GetOneLikePostService(likePost);

        if(LikePost is null){
            return false;
        }
        return true;
    }

}