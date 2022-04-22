using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models;

public class LikePost
{
    [Required(ErrorMessage = "userId is required.")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; } = null!;

    [Required(ErrorMessage = "postId is required.")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string postId { get; set; }= null!;

}