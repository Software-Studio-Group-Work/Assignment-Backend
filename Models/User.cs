using MongoDB.Bson;

using MongoDB.Bson.Serialization.Attributes;

namespace SoftStuApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string username { get; set; } = null!;

    public string password { get; set; }= null!;

    public string name { get; set; } = null!;

    public string picture { get; set; } = null!;
    public string religion { get; set; } = null!;
    public string role { get; set; } = null!;
    public bool isBan { get; set; } 



}