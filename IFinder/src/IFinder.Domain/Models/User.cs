using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IFinder.Domain.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Sex { get; set; }
    public int Age { get; set; }
    public string? Description { get; set; }
    public string? Hobbies { get; set; }

    public User()
    {
        
    }
}