using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models;

public class Place
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [Required(ErrorMessage = "adminId is required.")]  
    public string adminId { get; set; }= null!;

    [Required(ErrorMessage = "title is required.")]  
    public string title { get; set; } = null!;
    public string picture { get; set; } = null!;
    public string decription { get; set; } = null!;

}