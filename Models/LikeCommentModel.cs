using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Backend.Models;

public class LikeComment
{

    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; } = null!;
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string commentId { get; set; }= null!;

}