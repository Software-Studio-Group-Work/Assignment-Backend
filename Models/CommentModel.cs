using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string postId { get; set; }
    public string comment { get; set; } = null!;

    public string picture { get; set; } = null!;
    public bool isHide { get; set; }
}