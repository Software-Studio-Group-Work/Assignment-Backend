using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models;

public class LikeComment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }= ObjectId.GenerateNewId().ToString();
    
    [Required(ErrorMessage = "userId is required.")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; } = null!;

    [Required(ErrorMessage = "commentId is required.")]    
    [BsonRepresentation(BsonType.ObjectId)]
    public string commentId { get; set; }= null!;

}