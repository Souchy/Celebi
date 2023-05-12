using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "stats")] 
    public class StatsModelController : ControllerBase
    {
        private readonly CollectionService<IStats> _stats;
        public StatsModelController(MongoModelsDbService db)
        {
            _stats = db.GetMongoService<IStats>();
        }

        [HttpGet("all")]
        public async Task<List<IStats>> GetAll() => await _stats.GetAsync();

        [HttpGet("filtered")]
        public async Task<List<IStats>> GetFiltered(FilterDefinition<IStats> filter)
            => await _stats.GetAsync(filter);

        [HttpGet("{id}")]
        public async Task<ActionResult<IStats>> Get([FromRoute] ObjectId id)
        {
            IStats? stats = await _stats.GetOneAsync(id);
            if (stats is null)
                return NotFound();
            return Ok(stats);
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Post(Stats newStats)
        {
            await _stats.CreateAsync(newStats);
            return CreatedAtAction(nameof(Get), new { id = newStats.entityUid }, newStats);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] ObjectId id, Stats updatedStats)
        {
            var stats = await _stats.GetOneAsync(id);
            if (stats is null) 
                return NotFound();
            updatedStats.entityUid = stats.entityUid;
            var result = await _stats.UpdateAsync(id, updatedStats);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] ObjectId id)
        {
            var stats = await _stats.GetOneAsync(id);
            if (stats is null) 
                return NotFound();
            var result = await _stats.RemoveAsync(id);
            return Ok(result);
        }
    }
}
