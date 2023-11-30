
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IFinder.Domain.Models
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Text { get; set; }
        public string? IdUser { get; set; }
    }
}
