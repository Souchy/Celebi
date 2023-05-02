using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "creatures")] 
    public class CreatureModelController : ControllerBase
    {
        private readonly CreatureModelService _creatureModelService;

        public CreatureModelController(CreatureModelService service) => _creatureModelService = service;

        [HttpGet("all")]
        public async Task<List<ICreatureModel>> GetAll() => await _creatureModelService.GetAsync();

        [HttpGet("creature/{id}")]
        public async Task<ActionResult<ICreatureModel>> Get(string id)
        {
            ICreatureModel? creatureModel = await _creatureModelService.GetAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
        }

        [HttpPost("creature")]
        public async Task<IActionResult> Post(ICreatureModel newCreatureModel)
        {
            await _creatureModelService.CreateAsync(newCreatureModel);
            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }

        [HttpPut("creature/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(string id, ICreatureModel updatedCreatureModel)
        {
            var crea = await _creatureModelService.GetAsync(id);
            if (crea is null) 
                return NotFound();
            updatedCreatureModel.entityUid = crea.entityUid;
            var result = await _creatureModelService.UpdateAsync(id, updatedCreatureModel);
            return Ok(result);
        }

        [HttpDelete("creature/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(string id)
        {
            var crea = await _creatureModelService.GetAsync(id);
            if (crea is null) 
                return NotFound();
            var result = await _creatureModelService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
