using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TrailTracker.API.Data;
using TrailTracker.API.Models;

namespace TrailTracker.API.Services
{
    public class TrailService
    {
        private readonly IMongoCollection<Trail> _trails;

        public ITrailsRepository TrailsRepo;

        public TrailService(ITrailTrackerDatabaseSettings settings, ITrailsRepository trailsRepo)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _trails = database.GetCollection<Trail>(settings.TrailsCollectionName);
            TrailsRepo = trailsRepo;
        }

        public List<Trail> GetTrails()
        {
            var trails = TrailsRepo.GetTrails();

            return trails;
        }

        //public List<Trail> Get() => _trails.Find(t => true).ToList();

        public Trail Get(int id) => _trails.Find(t => t.Id == id).FirstOrDefault();

        public Trail Create(Trail trail)
        {
            _trails.InsertOne(trail);
            return trail;
        }

        public void Update(int id, Trail trailIn) => _trails.ReplaceOne(t => t.Id == id, trailIn);

        public void Remove(Trail trailIn) => _trails.DeleteOne(t => t.Id == trailIn.Id);

        public void Remove(int id) => _trails.DeleteOne(t => t.Id == id);
    }
}
