using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.factories;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "spells")]
    public class SpellModelController : ControllerBase
    {
        private readonly CollectionService<ISpellModel> _spellService;
        private readonly CollectionService<IStats> _stats;
        private readonly CollectionService<IEffect> _effects;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;

        public SpellModelController(MongoModelsDbService db, StringService strings, IDCounterService ids)
        {
            _spellService = db.GetMongoService<ISpellModel>();
            _stats = db.GetMongoService<IStats>();
            _effects = db.GetMongoService<IEffect>();
            _strings = strings;
            _ids = ids;
        }

        [HttpGet("all")]
        public async Task<List<ISpellModel>> GetAll() => await _spellService.GetAsync();

        [HttpGet("list")]
        public async Task<List<ISpellModel>> GetList(List<ObjectId> list) 
            => await _spellService.GetAsync(Builders<ISpellModel>.Filter.In(s => s.entityUid, list));

        [HttpGet("filtered")]
        public async Task<List<ISpellModel>> GetFiltered(FilterDefinition<ISpellModel> filter)
            => await _spellService.GetAsync(filter);

        [HttpGet("spell/{id}")]
        public async Task<ActionResult<ISpellModel>> Get([FromRoute] IID id)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            ISpellModel? model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            return Ok(model);
        }

        [Authorize]
        [HttpPost("spell")]
        public async Task<IActionResult> Post(SpellModel newSpellModel)
        {
            await _spellService.CreateAsync(newSpellModel);
            return CreatedAtAction(nameof(Get), new { id = newSpellModel.entityUid }, newSpellModel);
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> PostNew()
        {
            var spellModel = Factories.newSpellModel();

            spellModel.modelUid = await _ids.GetID<ICreatureModel>();
            spellModel.GetName().modelUid = await _ids.GetID<IStringEntity>();
            spellModel.GetDescription().modelUid = await _ids.GetID<IStringEntity>();

            await _stats.CreateAsync(spellModel.GetStats());
            await _strings.CreateAsync(spellModel.GetName());
            await _strings.CreateAsync(spellModel.GetDescription());
            await _spellService.CreateAsync(spellModel);

            return CreatedAtAction(nameof(Get), new { id = spellModel.entityUid }, spellModel);
        }

        [Authorize]
        [HttpPut("spell/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] IID id, SpellModel updatedSpellModel)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            updatedSpellModel.entityUid = model.entityUid;
            var result = await _spellService.UpdateAsync(filter, updatedSpellModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("spell/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] IID id)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            var result = await _spellService.RemoveAsync(filter);
            await _strings.RemoveAsync(model.nameId);
            await _strings.RemoveAsync(model.descriptionId);
            return Ok(result);
        }

    }
}
