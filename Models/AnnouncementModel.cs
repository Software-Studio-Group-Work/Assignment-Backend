using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

public class Announcement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string adminId { get; set; }
    public string title { get; set; } = null!;
    public string decription { get; set; } = null!;
    public string link{ get; set; } = null!;
}