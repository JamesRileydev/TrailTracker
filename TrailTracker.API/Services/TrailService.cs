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
            var trails = TrailsRepo.GetAllTrails().ConfigureAwait(false).GetAwaiter().GetResult();

            return trails;
        }

        public Trail GetTrail(int id)
        {
            var trail = TrailsRepo.GetTrail(id).ConfigureAwait(false).GetAwaiter().GetResult();

            return trail;
        }

        public Trail Get(int id) => _trails.Find(t => t.Id == id).FirstOrDefault();

        public Trail CreateTrail(Trail trail)
        {
            var response = TrailsRepo.CreateTrail(trail);
            return trail;
        }

        public void UpdateTrail(int id, Trail trailIn)
        {
            var response = TrailsRepo.UpdateTrail(id, trailIn);

            
        }// => TrailsRepo.UpdateTrail(t => t.Id == id, trailIn);


        public void DeleteTrail(int id)
        {
            var response = TrailsRepo.DeleteTrail(id);
        }
        public void Remove(Trail trailIn) => _trails.DeleteOne(t => t.Id == trailIn.Id);

        public void Remove(int id) => _trails.DeleteOne(t => t.Id == id);
    }
}
