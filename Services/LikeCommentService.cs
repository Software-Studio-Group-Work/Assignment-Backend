using Backend.Models;

using MongoDB.Driver;


namespace Backend.Services;

public class LikeCommentService{
        private readonly IMongoCollection<LikeComment> _likeCommentsCollection;
        
    public LikeCommentService(MongoDbService mongoDbService) {

        _likeCommentsCollection = mongoDbService.LikeCommentsCollection;
    }
    public async Task<List<LikeComment>> GetAllLikeCommentService() { 
        return await _likeCommentsCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<LikeComment>> GetLikesOnCommentService(string commendId ){
        return await _likeCommentsCollection.Find(x=>x.commentId==commendId).ToListAsync();
    }
    public async Task<LikeComment?> GetOneLikeCommentService(string likeCommentId ){
        return await _likeCommentsCollection.Find(x=>x.Id==likeCommentId).FirstOrDefaultAsync();
    }
    public async Task CreateOneLikeCommentService(LikeComment likeComment) { 
        await _likeCommentsCollection.InsertOneAsync(likeComment);
        return;
    }

    public async Task DeleteOneLikeCommentService(string likeCommentId) { 
        await _likeCommentsCollection.DeleteOneAsync(x=>x.Id==likeCommentId);
        return;
    }

    public async Task DeleteLikeCommentByUserService(string userId) { 
        FilterDefinition<LikeComment> filter= Builders<LikeComment>.Filter.Eq("userId", userId);
        await _likeCommentsCollection.DeleteManyAsync(filter);
        return;
    }

        public async Task DeleteLikeCommentByCommentService(string commentId) { 
        FilterDefinition<LikeComment> filter= Builders<LikeComment>.Filter.Eq("commentId", commentId);
        await _likeCommentsCollection.DeleteManyAsync(filter);
        return;
    }

        public bool likeCommentIsCreated(string likeCommentId){
         bool exists = _likeCommentsCollection.Find(_ => _.Id== likeCommentId).Any();
        return exists;
    }
        public bool likeCommentIsCreated(string userId,string commentId){
         bool exists = _likeCommentsCollection.Find(_ => _.userId== userId&&_.commentId== commentId).Any();
        return exists;
    }

}