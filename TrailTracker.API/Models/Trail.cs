using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TrailTracker.API.Models
{
    public class Trail
    {
        //private TrailsContext context;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public decimal Rating { get; set; }
    }
}
