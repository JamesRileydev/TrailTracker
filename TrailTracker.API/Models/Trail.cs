using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TrailTracker.API.Models
{
    public class Trail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Location { get; set; }

        public decimal Rating { get; set; }
    }
}
