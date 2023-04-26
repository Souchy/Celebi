using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.impl.shared;
using Spark.Services;

namespace Spark.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreatureModelController : ControllerBase
    {
        private readonly CreatureModelService _booksService;

        public CreatureModelController(CreatureModelService booksService) => _booksService = booksService;

        [HttpGet]
        public async Task<List<CreatureModel>> Get() => await _booksService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CreatureModel>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatureModel newCreatureModel)
        {
            await _booksService.CreateAsync(newCreatureModel);

            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, CreatureModel updatedCreatureModel)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedCreatureModel.entityUid = book.entityUid;

            await _booksService.UpdateAsync(id, updatedCreatureModel);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.RemoveAsync(id);

            return NoContent();
        }
    }
}
