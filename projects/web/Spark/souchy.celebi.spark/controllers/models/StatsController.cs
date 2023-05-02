using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "stats")] 
    public class StatsController : ControllerBase
    {
        private readonly StatsService _statsService;

        public StatsController(StatsService service) => _statsService = service;

        [HttpGet("all")]
        public async Task<List<IStats>> GetAll() => await _statsService.GetAsync();

        [HttpGet("stats/{id}")]
        public async Task<ActionResult<IStats>> Get(string id)
        {
            IStats? stats = await _statsService.GetAsync(id);
            if (stats is null)
                return NotFound();
            return Ok(stats);
        }

        [HttpPost("stats")]
        public async Task<IActionResult> Post(IStats newStats)
        {
            await _statsService.CreateAsync(newStats);
            return CreatedAtAction(nameof(Get), new { id = newStats.entityUid }, newStats);
        }

        [HttpPut("stats/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(string id, IStats updatedStats)
        {
            var stats = await _statsService.GetAsync(id);
            if (stats is null) 
                return NotFound();
            updatedStats.entityUid = stats.entityUid;
            var result = await _statsService.UpdateAsync(id, updatedStats);
            return Ok(result);
        }

        [HttpDelete("stats/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(string id)
        {
            var stats = await _statsService.GetAsync(id);
            if (stats is null) 
                return NotFound();
            var result = await _statsService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
