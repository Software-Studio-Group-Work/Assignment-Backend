namespace Backend.Models;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UsersCollectionName { get; set; } = null!;

    public string PostsCollectionName { get; set; } = null!;

    public string CommentsCollectionName { get; set; } = null!;
    public string AnnouncementsCollectionName { get; set; } = null!;
    public string LikeCommentsCollectionName { get; set; } = null!;
    public string LikePostsCollectionName { get; set; } = null!;
}