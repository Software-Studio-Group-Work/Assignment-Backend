using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models;

public class Post
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }= ObjectId.GenerateNewId().ToString();
    
    [Required(ErrorMessage = "userId is required.")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string  userId { get; set; }= null!;

    public string title { get; set; }= null!;

    public string description { get; set; } = null!;

    public string picture { get; set; } = null!;
    public string religion { get; set; } = null!;
    public bool isHide { get; set; } =false;


}