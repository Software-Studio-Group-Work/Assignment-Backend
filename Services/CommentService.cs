using Backend.Models;
using MongoDB.Driver;


namespace Backend.Services;

public class CommentService{
        private readonly IMongoCollection<Comment> _commentCollection;
        
    public CommentService(MongoDbService mongoDbService) {

        _commentCollection = mongoDbService.CommentsCollection;
    }
    public async Task<List<Comment>> GetAllCommentService() { 
        return await _commentCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<List<Comment>> GetPostCommentService(string postId) { 
        return await _commentCollection.Find(x=>x.postId==postId).ToListAsync();
        
    }
    public async Task<Comment?> GetOneCommentService(string commentId){
        return await _commentCollection.Find(x=>x.Id==commentId).FirstOrDefaultAsync();
    }
    public async Task CreateOneCommentService(Comment comment) { 
        await _commentCollection.InsertOneAsync(comment);
        return;
    }
    public async Task UpdateOneCommentService(string commentId, Comment updatedComment) {
        await _commentCollection.ReplaceOneAsync(x => x.Id == commentId, updatedComment);
        return;
    }
    public async Task DeleteOneCommentService(string commentId) { 
        await _commentCollection.DeleteOneAsync(x => x.Id == commentId);
        return;
    }
        public bool commentIsCreated(string commentId){
        var comment = GetOneCommentService(commentId);

        if(comment is null){
            return false;
        }
        return true;
    }

}