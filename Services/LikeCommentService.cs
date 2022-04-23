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
        public bool LikeCommentIsCreated(string likeCommentId){
        var LikeComment = GetOneLikeCommentService(likeCommentId);

        if(LikeComment is null){
            return false;
        }
        return true;
    }

}