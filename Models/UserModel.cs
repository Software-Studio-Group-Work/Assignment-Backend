using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models;


public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [Required(ErrorMessage = "username is required.")]
    public string username { get; set; } = null!;

    [Required(ErrorMessage = "password is required.")]
    public string password { get; set; } = null!;

    [Required(ErrorMessage = "name is required.")]
    public string name { get; set; } = null!;

    public string picture { get; set; } = "";
    public string religion { get; set; } = null!;

    [Required(ErrorMessage = "role is required.")]
    public string role { get; set; } = null!;
    public bool isBan { get; set; } = false;



}