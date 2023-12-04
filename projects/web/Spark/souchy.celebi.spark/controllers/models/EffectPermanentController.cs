using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Driver;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;
using System.Data;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "effect")] 
    public class EffectPermanentController : EffectContainerControllerBase<IEffect> // ControllerBase // 
    {

        //protected readonly CollectionService<IEffect> _effects;
        public EffectPermanentController(MongoModelsDbService db): base(db)
        {
            //_effects = db.GetMongoService<IEffect>();
        }

        [HttpGet("all")]
        public async Task<List<IEffect>> GetAll() => await _effects.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<IEffect>> Get(ObjectId id)
        {
            IEffect? effect = await _effects.GetOneAsync(id);
            if (effect is null)
                return NoContent();
            return Ok(effect);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("new")]
        public async Task<ActionResult<IEffectPermanent>> PostNew([FromQuery] string schemaName)
        {
            var eff = new EffectPermanent();
            eff.entityUid = ObjectId.GenerateNewId();
            eff.Schema = Enum.Parse<EffT>(schemaName).CreateSchema();
            eff.modelUid = (IID) (int) Enum.Parse<EffT>(schemaName);
            await _effects.CreateAsync(eff);
            //var eff = await base.newEffect(schemaName);
            return CreatedAtAction(nameof(Get), new { id = eff.entityUid }, eff);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("copy")]
        public async Task<ActionResult<IEffect>> pasteNew([FromBody] IEffect effect)
        {
            effect.entityUid = Eevee.RegisterIIDTemporary();
            await _effects.CreateAsync(effect);
            return CreatedAtAction(nameof(Get), new { id = effect.entityUid }, effect);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<IEffect>> Update(ObjectId id, IEffect effect)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null) 
                return NoContent();
            effect.entityUid = model.entityUid;
            var result = await _effects.UpdateAsync(id, effect);
            if (result.MatchedCount > 0) 
                return Ok(effect);
            return BadRequest();
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(ObjectId id)
        {
            var effect = await _effects.GetOneAsync(id);
            if (effect is null) 
                return NoContent();
            var result = await _effects.RemoveAsync(id);
            return Ok(result);
        }



        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}/schema")]
        public async Task<ActionResult<IEffect>> ChangeSchema([FromRoute] ObjectId id, [FromQuery] string schemaName)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null)
                return NoContent();

            model.Schema =  Enum.Parse<EffT>(schemaName).CreateSchema();
            model.modelUid = (IID) (int) Enum.Parse<EffT>(schemaName);
            var result = await _effects.UpdateAsync(model.entityUid, model);
            if (result.ModifiedCount > 0)
                return Ok(model);
            return BadRequest();
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}/triggers")]
        public async Task<ActionResult<UpdateResult>> UpdateTriggerList([FromRoute] ObjectId id, [FromBody] ITriggerModel[] triggers)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null)
                return NotFound();

            var set = new EntitySet<ITriggerModel>(triggers);
            var update = Builders<IEffect>.Update.Set(nameof(IEffect.Triggers), set);
            var result = await _effects.UpdateAsync(id, update);
            return Ok(result);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}/conditionTarget")]
        public async Task<ActionResult<UpdateResult>> UpdateConditionTarget([FromRoute] ObjectId id, [FromBody] ICondition targetFilter)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null)
                return NotFound();

            var update = Builders<IEffect>.Update.Set(nameof(IEffect.TargetFilter), targetFilter);
            var result = await _effects.UpdateAsync(id, update);
            return Ok(result);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}/conditionSource")]
        public async Task<ActionResult<UpdateResult>> UpdateConditionSource([FromRoute] ObjectId id, [FromBody] ICondition sourceCondition)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null)
                return NotFound();

            var update = Builders<IEffect>.Update.Set(nameof(IEffect.SourceCondition), sourceCondition);
            var result = await _effects.UpdateAsync(id, update);
            return Ok(result);
        }


    }
}
