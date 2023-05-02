using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "status")]
    public class StatusModelController : ControllerBase
    {
        private readonly StatusModelService _statusService;
        public StatusModelController(StatusModelService statuses) => _statusService = statuses;

        [HttpGet("all")]
        public async Task<List<IStatusModel>> GetAll() => await _statusService.GetAsync();

        [HttpGet("status/{id}")]
        public async Task<ActionResult<IStatusModel>> Get(string id)
        {
            IStatusModel? creatureModel = await _statusService.GetAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
        }

        [Authorize]
        [HttpPost("status")]
        public async Task<IActionResult> Post(StatusModel newSpellModel)
        {
            await _statusService.CreateAsync(newSpellModel);
            return CreatedAtAction(nameof(Get), new { id = newSpellModel.entityUid }, newSpellModel);
        }
        [Authorize]

        [HttpPut("status/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(string id, StatusModel updatedSpellModel)
        {
            var crea = await _statusService.GetAsync(id);
            if (crea is null)
                return NotFound();
            updatedSpellModel.entityUid = crea.entityUid;
            var result = await _statusService.UpdateAsync(id, updatedSpellModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("status/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(string id)
        {
            var crea = await _statusService.GetAsync(id);
            if (crea is null)
                return NotFound();
            var result = await _statusService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
