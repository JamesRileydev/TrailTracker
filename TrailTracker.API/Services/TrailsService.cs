﻿using Fody;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailTracker.API.Data;
using TrailTracker.API.Models;

namespace TrailTracker.API.Services
{
    [ConfigureAwait(false)]
    public class TrailsService
    {
        public ITrailsRepository TrailsRepo;

        private readonly ILogger Log;

        public TrailsService(ILogger logger, ITrailsRepository trailsRepo)
        {
            Log = logger.ForContext<TrailsService>(); ;
            TrailsRepo = trailsRepo;
        }

        public async ValueTask<(List<Trail>, ServiceError)> GetTrails()
        {
            Log.Information("Method called: {0}", nameof(GetTrails));

            var trails = new List<Trail>();

            try
            {
                trails = await TrailsRepo.GetAllTrails();

            }
            catch (Exception ex)
            {
                Log.Warning(ex, "An exception has occured while attempting to retrieve all trails");

                return (null, new ServiceError
                {
                    Message = "An error occured while attempt to retrieve all trails. See logs for details",
                    Exception = ex
                });
            }

            return (trails, null);
        }

        public async ValueTask<(Trail, ServiceError)> GetTrail(int id)
        {
            Log.Information("Method called: {0}", nameof(GetTrail));

            Trail trail;

            try
            {
                trail = await TrailsRepo.GetTrail(id);
            }
            catch (Exception ex)
            {
                Log.Warning("An error occured while attempting to retrieve trail with id: {id}", id);

                return (null, new ServiceError
                {
                    Message = $"An error occured while attempting to retrieve trail with id: {id}",
                    Exception = ex
                });
            }

            return (trail, null);
        }

        public async ValueTask<(int, ServiceError)> CreateTrail(Trail trail)
        {
            Log.Information("Method called: {0}", nameof(CreateTrail));

            int response;
            try
            {
                response = await TrailsRepo.CreateTrail(trail);
            }
            catch (Exception ex)
            {
                Log.Warning("An error occured while attempting to create trail: {name}", trail.Name);

                return (default, new ServiceError
                {
                    Message = $"An error occured while attempting to retrieve trail with name: {trail.Name}",
                    Exception = ex
                });
            }

            return (response, null);
        }

        public void UpdateTrail(int id, Trail trailIn)
        {
            Log.Information("Method called: {0}", nameof(UpdateTrail));

            TrailsRepo.UpdateTrail(id, trailIn);
        }

        public void DeleteTrail(int id)
        {
            Log.Information("Method called: {0}", nameof(DeleteTrail));


            TrailsRepo.DeleteTrail(id);
        }
    }
}