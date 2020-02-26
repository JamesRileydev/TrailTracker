﻿using Microsoft.AspNetCore.Mvc;
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

        }

        [HttpGet("{id}")]
        public ActionResult<Trail> GetTrail([FromRoute] int id)
        {
            var trail = _trailService.GetTrail(id);

            if (trail == null)
            {
                return NotFound();
            }

            return trail;
        }

        [HttpPost]
        public ActionResult<Trail> Create(Trail trail)
        {
            _trailService.CreateTrail(trail);

            return CreatedAtRoute("GetTrail", new { id = trail.Id.ToString() }, trail);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Trail trailIn)
        {
            var trail = _trailService.GetTrail(id);

            if (trail == null)
            {
                return NotFound();
            }

            _trailService.UpdateTrail(id, trailIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var trail = _trailService.GetTrail(id);

            if (trail == null)
            {
                return NotFound();
            }

            _trailService.DeleteTrail(trail.Id);
            return NoContent();
        }
    }
}
