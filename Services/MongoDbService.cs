using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services;

public class MongoDbService
{
    private readonly IMongoCollection<User> _usersCollection;

    private readonly IMongoCollection<Post> _postsCollection;

    private readonly IMongoCollection<Comment> _commentsCollection;

    private readonly IMongoCollection<Announcement> _announcementsCollection;

    private readonly IMongoCollection<LikeComment> _likeCommentsCollection;

    private readonly IMongoCollection<LikePost> _likePostsCollection;

    public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(
            mongoDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDbSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            mongoDbSettings.Value.UsersCollectionName);

        _postsCollection = mongoDatabase.GetCollection<Post>(
            mongoDbSettings.Value.PostsCollectionName);

        _commentsCollection = mongoDatabase.GetCollection<Comment>(
            mongoDbSettings.Value.CommentsCollectionName);

        _announcementsCollection = mongoDatabase.GetCollection<Announcement>(
            mongoDbSettings.Value.AnnouncementsCollectionName);

        _likeCommentsCollection = mongoDatabase.GetCollection<LikeComment>(
            mongoDbSettings.Value.LikeCommentsCollectionName);
        
        _likePostsCollection = mongoDatabase.GetCollection<LikePost>(
            mongoDbSettings.Value.LikePostsCollectionName);

    }

    public IMongoCollection<User> UsersCollection { get { return _usersCollection; } }

    public IMongoCollection<Post> PostsCollection { get { return _postsCollection; } }

    public IMongoCollection<Comment> CommentsCollection { get { return _commentsCollection; } }
    public IMongoCollection<Announcement> AnnouncementsCollection { get { return _announcementsCollection; } }
    public IMongoCollection<LikeComment> LikeCommentsCollection { get { return _likeCommentsCollection; } }
    public IMongoCollection<LikePost> LikePostsCollection { get { return _likePostsCollection; } }

}
