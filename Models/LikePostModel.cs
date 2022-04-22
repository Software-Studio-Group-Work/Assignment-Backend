using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

public class LikePost
{

    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string postId { get; set; }= null!;

}