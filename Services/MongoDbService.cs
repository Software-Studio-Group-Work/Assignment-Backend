using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services;

public class MongoDbService
{
    private readonly IMongoCollection<User> _usersCollection;

    private readonly IMongoCollection<Post> _postsCollection;

    private readonly IMongoCollection<Comment> _commentsCollection;

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

    }

    public IMongoCollection<User> UsersCollection { get { return _usersCollection; } }

    public IMongoCollection<Post> ContentsCollection { get { return _postsCollection; } }

    public IMongoCollection<Comment> CommentsCollection { get { return _commentsCollection; } }
}
