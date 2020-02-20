using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TrailTracker.API.Models;

namespace TrailTracker.API.Services
{
    public class TrailService
    {
        private readonly IMongoCollection<Trail> _trails;

        public TrailService(ITrailTrackerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _trails = database.GetCollection<Trail>(settings.TrailsCollectionName);
        }

        public List<Trail> Get() => _trails.Find(t => true).ToList();

        public Trail Get(string id) => _trails.Find(t => t.Id == id).FirstOrDefault();

        public Trail Create(Trail trail)
        {
            _trails.InsertOne(trail);
            return trail;                
        }

        public void Update(string id, Trail trailIn) => _trails.ReplaceOne(t => t.Id == id, trailIn);

        public void Remove(Trail trailIn) => _trails.DeleteOne(t => t.Id == trailIn.Id);

        public void Remove(string id) => _trails.DeleteOne(t => t.Id == id); 
    }
}
