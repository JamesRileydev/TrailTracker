using System.Collections.Generic;
using TrailTracker.API.Data;
using TrailTracker.API.Models;

namespace TrailTracker.API.Services
{
    public class TrailService
    {
        public ITrailsRepository TrailsRepo;

        public TrailService(ITrailsRepository trailsRepo)
        {
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

        public Trail CreateTrail(Trail trail)
        {
            var response = TrailsRepo.CreateTrail(trail);
            return trail;
        }

        public void UpdateTrail(int id, Trail trailIn)
        {
            var response = TrailsRepo.UpdateTrail(id, trailIn);
            
        }

        public void DeleteTrail(int id)
        {
            var response = TrailsRepo.DeleteTrail(id);
        }
    }
}
