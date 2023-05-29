using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.neweffects;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "effects")] 
    public class EffectPermanentController : ControllerBase
    {
        //private readonly EffectService _effectService;
        //public EffectModelController(EffectService service) => _effectService = service;
        private readonly CollectionService<IEffect> _effects;
        public EffectPermanentController(MongoModelsDbService db)
        {
            _effects = db.GetMongoService<IEffect>();
        }

        [HttpGet("all")]
        public async Task<List<IEffect>> GetAll() => await _effects.GetAsync();

        [HttpGet("effect/{id}")]
        public async Task<ActionResult<IEffect>> Get(ObjectId id)
        {
            IEffect? effect = await _effects.GetOneAsync(id);
            if (effect is null)
                return NotFound();
            return Ok(effect);
        }

        //[Authorize]
        [HttpPost("effect")]
        public async Task<ActionResult<IEffect>> Post(EffectPermanent newEffect)
        {
            await _effects.CreateAsync(newEffect);
            return CreatedAtAction(nameof(Get), new { id = newEffect.entityUid }, newEffect);
        }

        //[Authorize]
        [HttpPut("effect/{id}")]
        public async Task<ActionResult<IEffect>> Update(ObjectId id, EffectPermanent updatedEffect)
        {
            var effect = await _effects.GetOneAsync(id);
            if (effect is null) 
                return NotFound();
            updatedEffect.entityUid = effect.entityUid;
            var result = await _effects.UpdateAsync(id, updatedEffect);

            if (result.MatchedCount > 0) return Ok(updatedEffect);
            else return Ok(effect);
        }

        //[Authorize]
        [HttpDelete("effect/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(ObjectId id)
        {
            var effect = await _effects.GetOneAsync(id);
            if (effect is null) 
                return NotFound();
            var result = await _effects.RemoveAsync(id);
            return Ok(result);
        }


        [HttpPost("{id}/child")]
        public async Task<ActionResult<IEffect>> AddEffect([FromRoute] ObjectId id, [FromQuery] string schemaName)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null)
                return NotFound();

            var schemaType = typeof(IEntity).Assembly
                .GetTypes().FirstOrDefault(t => t.Name == schemaName);
            if (schemaType == null)
                return NotFound();

            var eff = new EffectPermanent();
            eff.entityUid = ObjectId.GenerateNewId();
            eff.Schema = (IEffectSchema) Activator.CreateInstance(schemaType)!;
            eff.modelUid = (IID) (int) Enum.Parse<EffT>(schemaName);

            model.EffectIds.Add(eff.entityUid);
            await _effects.CreateAsync(eff);
            var result = await _effects.UpdateAsync(model.entityUid, model);

            return Ok(model);
        }

    }
}
