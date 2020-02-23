using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TrailTracker.API.Models;
using TrailTracker.API.Services;

namespace TrailTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly TrailService _trailService;

        public TrailsController(TrailService trailService)
        {
            _trailService = trailService;
        }

        [HttpGet]
        public ActionResult<List<Trail>> GetTrails()
        {
            var trails = _trailService.GetTrails();

            return trails;

            //var context = HttpContext.RequestServices.GetService(typeof(TrailsContext)) as TrailsContext;
            //return context.GetAllTrails();
        }

        //[HttpGet]
        //public ActionResult<List<Trail>> Get() => _trailService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTrail")]
        public ActionResult<Trail> Get(int id)
        {
            var trail = _trailService.Get(id);

            if (trail == null)
            {
                return NotFound();
            }

            return trail;
        }

        [HttpPost]
        public ActionResult<Trail> Create(Trail trail)
        {
            _trailService.Create(trail);

            return CreatedAtRoute("GetTrail", new { id = trail.Id.ToString() }, trail);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(int id, Trail trailIn)
        {
            var trail = _trailService.Get(id);

            if (trail == null)
            {
                return NotFound();
            }

            _trailService.Update(id, trailIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(int id)
        {
            var trail = _trailService.Get(id);

            if (trail == null)
            {
                return NotFound();
            }

            _trailService.Remove(trail.Id);
            return NoContent();
        }
    }
}
