using AutofacContrib.NSubstitute;
using Fody;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailTracker.API.Controllers;
using TrailTracker.API.Models;
using TrailTracker.API.Services;
using Xunit;

namespace TrailTracker.API.Tests
{
    [ConfigureAwait(false)]
    [Trait("Category", "Unit")]
    public class TrailsControllerUnitTests
    {
        private readonly string _error = "error";

        [Fact]
        public async Task CreateTrail_ReturnsServiceError_WhenFailsToCreateTrail()
        {
            var autoSub = new AutoSubstitute();

            autoSub.Resolve<TrailsController>();
            var trailSvc = autoSub.Resolve<ITrailsService>();

            trailSvc.CreateTrail(Arg.Any<Trail>()).Returns((default, new ServiceError { Message = _error }));

            var result = await autoSub.Resolve<TrailsController>().CreateTrail(new Trail());

            Assert.NotNull(result);
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
            Assert.Equal(_error, errorResult.Value);
        }

        [Fact]
        public async Task CreateTrail_ReturnsCreated_WhenSuccessfullyCreatesTrail()
        {
            var autoSub = new AutoSubstitute();

            autoSub.Resolve<TrailsController>();
            var trailSvc = autoSub.Resolve<ITrailsService>();

            trailSvc.CreateTrail(Arg.Any<Trail>()).Returns((1, null));

            var result = await autoSub.Resolve<TrailsController>().CreateTrail(new Trail());

            Assert.NotNull(result);
            var successResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(StatusCodes.Status201Created, successResult.StatusCode);
        }

        [Fact]
        public async Task GetTrails_ReturnsServiceError_WhenFailsToGetTrails()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrails().Returns((null, new ServiceError{Message = _error}));

            var result = await autosub.Resolve<TrailsController>().GetTrails();

            Assert.NotNull(result);
            var error = Assert.IsType<ObjectResult>(result);
            Assert.Equal(_error, error.Value); 
        }

        [Fact]
        public async Task GetTrails_ReturnsOk_WhenSuccessfullyGetsTrails()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrails().Returns((new List<Trail>(), null));

            var result = await autosub.Resolve<TrailsController>().GetTrails();

            Assert.NotNull(result);
            var success = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, success.StatusCode); 
        }

        [Fact]
        public async Task GetTrail_ReturnsServiceError_WhenFailsToGetTrailById()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((null, new ServiceError { Message = _error }));

            var result = await autosub.Resolve<TrailsController>().GetTrail(1);

            Assert.NotNull(result);
            var error = Assert.IsType<ObjectResult>(result);
            Assert.Equal("error", error.Value);
        }

        [Fact]
        public async Task GetTrail_ReturnsNotFound_WhenTrailDoesNotExist()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((null, null));

            var result = await autosub.Resolve<TrailsController>().GetTrail(1);

            Assert.NotNull(result);
            var error = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task GetTrail_ReturnsOk_WhenSuccessfullyGetsTrailById()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((new Trail(), null));

            var result = await autosub.Resolve<TrailsController>().GetTrail(1);

            Assert.NotNull(result);
            var success = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, success.StatusCode);
        }

        [Fact]
        public async Task UpdateTrail_ReturnsServiceError_WhenServiceFailsUpdate()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((null, new ServiceError { Message = _error }));

            var result = await autosub.Resolve<TrailsController>().UpdateTrail(1, new Trail());

            Assert.NotNull(result);
            var error = Assert.IsType<ObjectResult>(result);
            Assert.Equal("error", error.Value);
        }

        [Fact]
        public async Task UpdateTrail_ReturnsNotFound_WhenTrailDoesNotExist()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((null, null));

            var result = await autosub.Resolve<TrailsController>().UpdateTrail(1, new Trail());

            Assert.NotNull(result);
            var error = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task UpdateTrail_ReturnsNoContent_WhenSuccessfullyUpdatesTrailById()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((new Trail(), null));

            var result = await autosub.Resolve<TrailsController>().UpdateTrail(1, new Trail());

            Assert.NotNull(result);
            var success = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, success.StatusCode);
        }

        [Fact]
        public async Task DeleteTrail_ReturnsServiceError_WhenGetTrailServiceFails()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((null, new ServiceError { Message = _error }));

            var result = await autosub.Resolve<TrailsController>().DeleteTrail(1);

            Assert.NotNull(result);
            var error = Assert.IsType<ObjectResult>(result);
            Assert.Equal("error", error.Value);
        }

        [Fact]
        public async Task DeleteTrail_ReturnsNotFound_WhenTrailDoesNotExist()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((null, null));

            var result = await autosub.Resolve<TrailsController>().DeleteTrail(1);

            Assert.NotNull(result);
            var error = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task DeleteTrail_ReturnsNoContent_WhenSuccessfullyDeletesTrailById()
        {
            var autosub = new AutoSubstitute();

            var trailSvc = autosub.Resolve<ITrailsService>();
            trailSvc.GetTrail(Arg.Any<int>()).Returns((new Trail(), null));

            var result = await autosub.Resolve<TrailsController>().DeleteTrail(1);

            Assert.NotNull(result);
            var success = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, success.StatusCode);
        }
    }
}
