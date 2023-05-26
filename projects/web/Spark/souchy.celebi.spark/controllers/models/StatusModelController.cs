using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "status")]
    public class StatusModelController : ControllerBase
    {
        private readonly CollectionService<IStatusModel> _statusService;
        public StatusModelController(MongoModelsDbService db)
        {
            _statusService = db.GetMongoService<IStatusModel>();
        }

        [HttpGet("all")]
        public async Task<List<IStatusModel>> GetAll() => await _statusService.GetAsync();

        [HttpGet("status/{id}")]
        public async Task<ActionResult<IStatusModel>> Get(ObjectId id)
        {
            IStatusModel? creatureModel = await _statusService.GetOneAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
        }

        //[Authorize]
        [HttpPost("status")]
        public async Task<IActionResult> Post(StatusModel newSpellModel)
        {
            await _statusService.CreateAsync(newSpellModel);
            return CreatedAtAction(nameof(Get), new { id = newSpellModel.entityUid }, newSpellModel);
        }

        //[Authorize]
        [HttpPut("status/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(ObjectId id, StatusModel updatedSpellModel)
        {
            var crea = await _statusService.GetOneAsync(id);
            if (crea is null)
                return NotFound();
            updatedSpellModel.entityUid = crea.entityUid;
            var result = await _statusService.UpdateAsync(id, updatedSpellModel);
            return Ok(result);
        }

        //[Authorize]
        [HttpDelete("status/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(ObjectId id)
        {
            var crea = await _statusService.GetOneAsync(id);
            if (crea is null)
                return NotFound();
            var result = await _statusService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
