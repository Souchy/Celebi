using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services;

namespace souchy.celebi.spark.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreatureModelController : ControllerBase
    {
        private readonly CreatureModelService creatureModelService;

        public CreatureModelController(CreatureModelService service)
        {
            creatureModelService = service;
        }

        [HttpGet]
        public async Task<List<ICreatureModel>> Get() => await creatureModelService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ICreatureModel>> Get(string id)
        {
            ICreatureModel? creatureModel = await creatureModelService.GetAsync(id);

            if (creatureModel is null)
                return NotFound();

            return (CreatureModel) creatureModel;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ICreatureModel newCreatureModel)
        {
            await creatureModelService.CreateAsync(newCreatureModel);

            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, CreatureModel updatedCreatureModel)
        {
            var book = await creatureModelService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedCreatureModel.entityUid = book.entityUid;

            await creatureModelService.UpdateAsync(id, updatedCreatureModel);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await creatureModelService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await creatureModelService.RemoveAsync(id);

            return NoContent();
        }
    }
}
