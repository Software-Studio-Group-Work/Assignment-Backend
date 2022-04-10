using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("username")]
    public string username { get; set; } = null!;

    [BsonElement("password")]
    public string password { get; set; } = null!;

    [BsonElement("name")]
    public string name { get; set; } = null!;

    [BsonElement("picture")]
    public string picture { get; set; } = null!;

    [BsonElement("religion")]
    public string religion { get; set; } = null!;

    [BsonElement("role")]
    public string role { get; set; } = null!;

    [BsonElement("enable")]
    public bool enable { get; set; } = true;

}