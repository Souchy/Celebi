using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.neweffects;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.eevee.neweffects.impl.effects;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "spell")]
    public class SpellModelController : ControllerBase
    {
        private readonly CollectionService<ISpellModel> _spellService;
        private readonly CollectionService<IStats> _stats;
        private readonly CollectionService<IEffect> _effects;
        private readonly CollectionService<ISpellSkin> _skins;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;
        private readonly MongoFederationService _federation;

        public SpellModelController(MongoModelsDbService db, StringService strings, IDCounterService ids, MongoFederationService federation)
        {
            _spellService = db.GetMongoService<ISpellModel>();
            _stats = db.GetMongoService<IStats>();
            _effects = db.GetMongoService<IEffect>();
            _skins = db.GetMongoService<ISpellSkin>();
            _strings = strings;
            _ids = ids;
            _federation = federation;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ISpellModel>>> GetAll()
        {
            var list = await _spellService.GetAsync();
            return Ok(list);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ISpellModel>>> GetList([FromQuery] List<ObjectId> list) 
            => Ok(await _spellService.GetAsync(Builders<ISpellModel>.Filter.In("_id", list)));

        [HttpGet("filtered")]
        public async Task<ActionResult<List<ISpellModel>>> GetFiltered(FilterDefinition<ISpellModel> filter)
            => Ok(await _spellService.GetAsync(filter));

        [HttpGet("byString/{str}")]
        public async Task<IEnumerable<ISpellModel>> GetByString(string str) => await _federation.FindSpellsByString(str);

        [HttpGet("{id}")]
        public async Task<ActionResult<ISpellModel>> Get([FromRoute] SpellIID id)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id.value);
            ISpellModel? model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            return Ok(model);
        }

        //[Authorize]
        [HttpPost("")]
        public async Task<ActionResult<ISpellModel>> Post([FromBody] SpellModel newSpellModel)
        {
            await _spellService.CreateAsync(newSpellModel);
            return CreatedAtAction(nameof(Get), new { id = newSpellModel.entityUid }, newSpellModel);
        }

        //[Authorize]
        [HttpPost("new")]
        public async Task<ActionResult<ISpellModel>> PostNew()
        {
            var model = await Factories.newSpellModel(_ids);

            await _spellService.CreateAsync(model.spell);
            await _strings.CreateAsync(model.name);
            await _strings.CreateAsync(model.desc);
            await _stats.CreateAsync(model.stats);
            await _skins.CreateAsync(model.skin);

            return CreatedAtAction(nameof(Get), new { id = model.spell.entityUid }, model.spell);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ISpellModel>> Update([FromRoute] SpellIID id, [FromBody] SpellModel updatedModel)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id.value);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            updatedModel.entityUid = model.entityUid;
            var result = await _spellService.UpdateAsync(filter, updatedModel);
            if(result.MatchedCount > 0) return Ok(updatedModel);
            else return Ok(model);
        }

        [HttpPost("{id}/effect")]
        public async Task<ActionResult<ISpellModel>> AddEffect([FromRoute] SpellIID id, [FromQuery] ObjectId? effectParentId, [FromQuery] string schemaName)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id.value);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();

            var schemaType = typeof(IEntity).Assembly
                .GetTypes().FirstOrDefault(t => t.Name == schemaName);
            if(schemaType == null)
                return NotFound();

            var eff = new EffectPermanent();
            eff.entityUid = ObjectId.GenerateNewId();
            eff.Schema = (IEffectSchema) Activator.CreateInstance(schemaType)!;
            eff.modelUid = (IID) (int) Enum.Parse<EffT>(schemaName);

            if(!addEffectToParent(model, eff, effectParentId))
            {
                model.EffectIds.Add(eff.entityUid);
            }

            await _effects.CreateAsync(eff);
            var result = await _spellService.UpdateAsync(model.entityUid, model);

            return Ok(model);
        }
        private bool addEffectToParent(IEffectsContainer container, IEffect eff, ObjectId? parentId)
        {
            if (parentId == null || parentId != ObjectId.Empty || container == null || container.EffectIds == null || container.EffectIds.Values.Count == 0) 
                return false;

            foreach (var child in container.GetEffects())
            {
                if (child.entityUid == parentId)
                {
                    child.EffectIds.Add(eff.entityUid);
                    return true;
                }
                else
                if (addEffectToParent(child, eff, parentId))
                {
                    return true;
                }
            }
            return false;
        }


        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] SpellIID id)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id.value);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            var result = await _spellService.RemoveAsync(filter);
            await _strings.RemoveAsync(model.nameId);
            await _strings.RemoveAsync(model.descriptionId);
            await _stats.RemoveAsync(model.statsId);
            return Ok(result);
        }

    }
}
