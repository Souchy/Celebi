using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "spells")]
    public class SpellModelController : ControllerBase
    {
        private readonly SpellModelService _spellService;
        public SpellModelController(SpellModelService spells) => _spellService = spells;

        [HttpGet("all")]
        public async Task<List<ISpellModel>> GetAll() => await _spellService.GetAsync();

        [HttpGet("spell/{id}")]
        public async Task<ActionResult<ISpellModel>> Get(string id)
        {
            ISpellModel? creatureModel = await _spellService.GetAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
        }

        [Authorize]
        [HttpPost("spell")]
        public async Task<IActionResult> Post(SpellModel newSpellModel)
        {
            await _spellService.CreateAsync(newSpellModel);
            return CreatedAtAction(nameof(Get), new { id = newSpellModel.entityUid }, newSpellModel);
        }

        [Authorize]
        [HttpPut("spell/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(string id, SpellModel updatedSpellModel)
        {
            var crea = await _spellService.GetAsync(id);
            if (crea is null)
                return NotFound();
            updatedSpellModel.entityUid = crea.entityUid;
            var result = await _spellService.UpdateAsync(id, updatedSpellModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("spell/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(string id)
        {
            var crea = await _spellService.GetAsync(id);
            if (crea is null)
                return NotFound();
            var result = await _spellService.RemoveAsync(id);
            return Ok(result);
        }

    }
}
