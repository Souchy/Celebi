﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ISpellModel>> Get([FromRoute] IID id)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            ISpellModel? model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            return Ok(model);
        }

        [Authorize]
        [HttpPost("")]
        public async Task<ActionResult<ISpellModel>> Post([FromBody] SpellModel newSpellModel)
        {
            await _spellService.CreateAsync(newSpellModel);
            return CreatedAtAction(nameof(Get), new { id = newSpellModel.entityUid }, newSpellModel);
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<ActionResult<ISpellModel>> PostNew()
        {
            (ISpellModel spell, IStringEntity name, IStringEntity desc, IStats stats) model = await Factories.newSpellModel(_ids);

            await _spellService.CreateAsync(model.spell);
            await _strings.CreateAsync(model.name);
            await _strings.CreateAsync(model.desc);
            await _stats.CreateAsync(model.stats);

            return CreatedAtAction(nameof(Get), new { id = model.spell.entityUid }, model.spell);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] IID id, [FromBody] SpellModel updatedSpellModel)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            updatedSpellModel.entityUid = model.entityUid;
            var result = await _spellService.UpdateAsync(filter, updatedSpellModel);
            return Ok(result);
        }

        [HttpPost("{id}/effect")]
        public async Task<ActionResult<ReplaceOneResult>> AddEffect([FromRoute] IID id, [FromQuery] ObjectId? effectParentId, [FromQuery] string schemaName)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
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

            return Ok(result);
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

        [HttpPut("{id}/{effectParentId}")]
        public async Task<ActionResult<UpdateResult>> AddEffect([FromRoute] IID id, [FromRoute] ObjectId effectParentId, [FromBody] Effect effect)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            if(effect.entityUid == null || effect.entityUid == ObjectId.Empty)
            {
                effect.entityUid = ObjectId.GenerateNewId();
                await _effects.CreateAsync(effect);
                model.EffectIds.Add(effect.entityUid);
            }
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] IID id)
        {
            var filter = Builders<ISpellModel>.Filter.Eq(nameof(ISpellModel.modelUid), id);
            var model = await _spellService.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            var result = await _spellService.RemoveAsync(filter);
            await _strings.RemoveAsync(model.nameId);
            await _strings.RemoveAsync(model.descriptionId);
            await _stats.RemoveAsync(model.stats);
            return Ok(result);
        }

    }
}
