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
    public async Task<LikeComment?> GetOneLikeCommentService(LikeComment likeComment ){
        return await _likeCommentsCollection.Find(x=>(x.userId==likeComment.userId&&x.commentId==likeComment.commentId)).FirstOrDefaultAsync();
    }
    public async Task CreateOneLikeCommentService(LikeComment likeComment) { 
        await _likeCommentsCollection.InsertOneAsync(likeComment);
        return;
    }

    public async Task DeleteOneLikeCommentService(LikeComment likeComment) { 
        await _likeCommentsCollection.DeleteOneAsync(x =>(x.userId==likeComment.userId&&x.commentId==likeComment.commentId));
        return;
    }
        public bool LikeCommentIsCreated(LikeComment likeComment){
        var LikeComment = GetOneLikeCommentService(likeComment);

        if(LikeComment is null){
            return false;
        }
        return true;
    }

}