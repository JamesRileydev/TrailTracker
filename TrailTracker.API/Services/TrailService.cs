using Serilog;
using System;
using System.Collections.Generic;
using TrailTracker.API.Data;
using TrailTracker.API.Models;

namespace TrailTracker.API.Services
{
    public class TrailService
    {
        public ITrailsRepository TrailsRepo;

        private readonly ILogger Log;

        public TrailService(ILogger logger, ITrailsRepository trailsRepo)
        {
            Log = logger.ForContext<TrailService>(); ;
            TrailsRepo = trailsRepo;
        }

        public (List<Trail>, ServiceError) GetTrails()
        {
            Log.Information("Attempting to {0}", nameof(GetTrails));

            var trails = new List<Trail>();

            try
            {
            trails = TrailsRepo.GetAllTrails().ConfigureAwait(false).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                Log.Warning(ex,"An exception has occured while attempting to retrieve all trails");

                return (null, new ServiceError
                {
                    Message = "An error occured while attempt to retrieve all trails. See logs for details",
                    Description = "",
                    Exception = ex
                });
            }

            return (trails, null);
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
