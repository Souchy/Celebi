using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.factories;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;
namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "creatures")] 
    public class CreatureModelController : ControllerBase
    {
        private readonly CreatureModelService _creatureModelService;
        private readonly StatsService _stats;
        private readonly StringService _strings;

        public CreatureModelController(CreatureModelService service, StatsService stats, StringService strings)
        {
            _creatureModelService = service;
            _stats = stats;
            _strings = strings;
        }

        [HttpGet("all")]
        public async Task<List<ICreatureModel>> GetAll() => await _creatureModelService.GetAsync();

        [HttpGet("creature/{id}")]
        public async Task<ActionResult<ICreatureModel>> Get([FromRoute] IID id)
        {
            var creatureModel = await _creatureModelService.GetAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
        }

        //[Authorize]
        [HttpPost("creature")]
        public async Task<ActionResult<ICreatureModel>> Post(CreatureModel newCreatureModel)
        {
            await _creatureModelService.CreateAsync(newCreatureModel);
            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }

        [HttpPost("new")]
        public async Task<ActionResult<ICreatureModel>> PostNew()
        {
            var resources = Resource.values.Values;
            var characs = Enumerable.OfType<CharacteristicType>(resources);

            var newCreatureModel = Factories.newCreatureModel();

            await _stats.CreateAsync(newCreatureModel.GetBaseStats());
            await _stats.CreateAsync(newCreatureModel.GetGrowthStats());
            await _strings.CreateAsync(newCreatureModel.GetName());
            await _strings.CreateAsync(newCreatureModel.GetDescription());
            await _creatureModelService.CreateAsync(newCreatureModel);

            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }
        

        [Authorize]
        [HttpPut("creature/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] IID id, [FromBody] CreatureModel updatedCreatureModel)
        {
            var crea = await _creatureModelService.GetAsync(id);
            if (crea is null) 
                return NotFound();
            updatedCreatureModel.entityUid = crea.entityUid;
            var result = await _creatureModelService.UpdateAsync(id, updatedCreatureModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("creature/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] IID id)
        {
            var crea = await _creatureModelService.GetAsync(id);
            if (crea is null) 
                return NotFound();
            var result = await _creatureModelService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
