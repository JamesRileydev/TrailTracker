using Fody;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrailTracker.API.Models;
using TrailTracker.API.Services;

namespace TrailTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ConfigureAwait(false)]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailsService _trailService;

        public TrailsController(ITrailsService trailService)
        {
            _trailService = trailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrail([FromBody]Trail trail)
        {
            var (createdId, error) = await _trailService.CreateTrail(trail);

            if (error != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

            var result = Created("GetTrail", new { id = createdId.ToString() });
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrails()
        {
            var (trails, error) = await _trailService.GetTrails();

            if (error != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

            return Ok(trails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrail([FromRoute] int id)
        {
            var (trail, error) = await _trailService.GetTrail(id);

            if (error != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

            if (trail == null)
            {
                return NotFound();
            }

            return Ok(trail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrail(int id, Trail trailIn)
        {
            var (trail, error) = await _trailService.GetTrail(id);

            if (error != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

            if (trail == null)
            {
                return NotFound();
            }

            _trailService.UpdateTrail(id, trailIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrail(int id)
        {
            var (trail, error) = await _trailService.GetTrail(id);

            if (error != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

            if (trail == null)
            {
                return NotFound();
            }

            _trailService.DeleteTrail(id);
            return NoContent();
        }
    }
}
