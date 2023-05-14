using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;
using System.Data;

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
        public async Task<IActionResult> Post([FromBody] Stats newStats)
        {
            await _stats.CreateAsync(newStats);
            return CreatedAtAction(nameof(Get), new { id = newStats.entityUid }, newStats);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] ObjectId id, [FromBody] Stats updatedStats)
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


        //[Authorize]
        [HttpPut("{id}/bool")]
        public async Task<ActionResult<UpdateResult>> UpdateStatBool([FromRoute] ObjectId id, [FromBody] StatBool updatedStats)
        {
            var result = await UpdateStatus(id, updatedStats);
            if (result == null) return NotFound("Missing Stats object");
            return Ok(result);
        }

        //[Authorize]
        [HttpPut("{id}/simple")]
        public async Task<ActionResult<UpdateResult>> UpdateStatSimple([FromRoute] ObjectId id, [FromBody] StatSimple updatedStats)
        {
            var result = await UpdateStatus(id, updatedStats);
            if (result == null) return NotFound("Missing Stats object");
            return Ok(result);
        }
        private async Task<UpdateResult> UpdateStatus(ObjectId id, IStat updatedStats)
        {
            var stats = await _stats.GetOneAsync(id);
            if (stats is null)
                return null; // NotFound();
            if (stats.Has(updatedStats.statId))
            {
                var original = stats.Get(updatedStats.statId);
                updatedStats.entityUid = original.entityUid;
            }
            else
            {
                updatedStats.entityUid = Eevee.RegisterIIDTemporary();
            }
            var filter = _stats.filterId(stats.entityUid);
            var update = Builders<IStats>.Update.Set(Stats.dicName + "." + updatedStats.statId, updatedStats);
            var result = await _stats.Collection.UpdateOneAsync(filter, update);
            return result;
        }



    }
}
