using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models;

public class LikePost
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }= ObjectId.GenerateNewId().ToString();
    
    [Required(ErrorMessage = "userId is required.")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; } = null!;

    [Required(ErrorMessage = "postId is required.")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string postId { get; set; }= null!;

}