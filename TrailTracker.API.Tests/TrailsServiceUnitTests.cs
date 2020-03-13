using AutofacContrib.NSubstitute;
using Fody;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailTracker.API.Data;
using TrailTracker.API.Models;
using TrailTracker.API.Services;
using Xunit;

namespace TrailTracker.API.Tests
{
    [ConfigureAwait(false)]
    [Trait("Category", "Unit")]
    public class TrailsServiceUnitTests
    {
        [Fact]
        public async Task GetTrails_ReturnsServiceError_WhenRepoCallFails()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.GetAllTrails().Throws(new Exception("Error retrieving trails"));

            var (result, error) = await autoSub.Resolve<TrailsService>().GetTrails();

            Assert.Null(result);
            Assert.NotNull(error);
            Assert.Equal("Error retrieving trails", error.Exception.Message);
        }

        [Fact]
        public async Task GetTrails_ReturnsList_WhenSuccessful()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.GetAllTrails().Returns(new List<Trail> { new Trail()});

            var (result, error) = await autoSub.Resolve<TrailsService>().GetTrails();

            Assert.NotNull(result);
            Assert.Null(error);
            Assert.IsType<List<Trail>>(result);
        }

        [Fact]
        public async Task GetTrail_ReturnsServiceError_WhenRepoCallFails()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.GetTrail(Arg.Any<int>()).Throws(new Exception("Error retrieving trail"));

            var (result, error) = await autoSub.Resolve<TrailsService>().GetTrail(1);

            Assert.Null(result);
            Assert.NotNull(error);
            Assert.Equal("Error retrieving trail", error.Exception.Message);
        }

        [Fact]
        public async Task GetTrail_ReturnsTrail_WhenSuccessful()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.GetTrail(Arg.Any<int>()).Returns( new Trail() );

            var (result, error) = await autoSub.Resolve<TrailsService>().GetTrail(1);

            Assert.NotNull(result);
            Assert.Null(error);
            Assert.IsType<Trail>(result);
        }

        [Fact]
        public async Task CreateTrail_ReturnsServiceError_WhenRepoCallFails()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.CreateTrail(Arg.Any<Trail>()).Throws(new Exception("Error creating trail"));

            var (result, error) = await autoSub.Resolve<TrailsService>().CreateTrail(new Trail());

            Assert.Equal(0, result);
            Assert.NotNull(error);
            Assert.Equal("Error creating trail", error.Exception.Message);
        }

        [Fact]
        public async Task CreateTrail_ReturnsShiny_WhenSuccessful()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.CreateTrail(Arg.Any<Trail>()).Returns(1);

            var (result, error) = await autoSub.Resolve<TrailsService>().CreateTrail(new Trail());

            Assert.Equal(1, result);
            Assert.Null(error);
            Assert.IsType<int>(result);
        }

        [Fact]
        public void DeleteTrail_Just_Works()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.DeleteTrail(Arg.Any<int>());

            autoSub.Resolve<TrailsService>().DeleteTrail(1);
        }

        [Fact]
        public void UpdateTrail_Just_Works()
        {
            var autoSub = new AutoSubstitute();

            var trailsRepo = autoSub.Resolve<ITrailsRepository>();
            trailsRepo.UpdateTrail(Arg.Any<int>(), Arg.Any<Trail>());

            autoSub.Resolve<TrailsService>().UpdateTrail(1, new Trail());
        }
    }
}
