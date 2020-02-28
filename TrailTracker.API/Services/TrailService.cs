using Microsoft.Extensions.Logging;
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
        private ILogger<TrailService> logger;

        public TrailService(ILogger<TrailService> logger, ITrailsRepository trailsRepo)
        {
            this.logger = logger;
            TrailsRepo = trailsRepo;

        }

        public (List<Trail>, ServiceError) GetTrails()
        {
            logger.LogInformation("Get Trails");

            var trails = new List<Trail>();

            try
            {
            trails = TrailsRepo.GetAllTrails().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                return (null, new ServiceError
                {
                    Message = "An error occured while attempt to retrieve all trails",
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
