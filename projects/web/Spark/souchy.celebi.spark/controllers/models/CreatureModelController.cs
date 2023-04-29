using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "creature")] //  [Route(Routes.Models + "[controller]")]
    public class CreatureModelController : ControllerBase
    {
        private readonly CreatureModelService _creatureModelService;

        public CreatureModelController(CreatureModelService service) => _creatureModelService = service;

        [HttpGet("creatures")]
        public async Task<List<ICreatureModel>> GetAll() => await _creatureModelService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ICreatureModel>> Get(string id)
        {
            ICreatureModel? creatureModel = await _creatureModelService.GetAsync(id);

            if (creatureModel is null)
                return NotFound();

            return (CreatureModel) creatureModel;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(ICreatureModel newCreatureModel)
        {
            await _creatureModelService.CreateAsync(newCreatureModel);

            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ICreatureModel updatedCreatureModel)
        {
            var book = await _creatureModelService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedCreatureModel.entityUid = book.entityUid;

            await _creatureModelService.UpdateAsync(id, updatedCreatureModel);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _creatureModelService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _creatureModelService.RemoveAsync(id);

            return NoContent();
        }
    }
}
