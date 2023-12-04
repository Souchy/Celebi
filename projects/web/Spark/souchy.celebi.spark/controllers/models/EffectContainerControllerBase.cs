using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;

namespace souchy.celebi.spark.controllers.models
{
    public abstract class EffectContainerControllerBase<T> : ControllerBase where T : IEffectsContainer, IEntity
    {

        protected readonly CollectionService<IEffect> _effects;
        protected readonly CollectionService<T> _containers;
        public EffectContainerControllerBase(MongoModelsDbService db)
        {
            _effects = db.GetMongoService<IEffect>();
            _containers = db.GetMongoService<T>();
        }

        protected async Task<IEffect> newEffect(string schemaName)
        {
            var eff = new EffectPermanent();
            eff.entityUid = ObjectId.GenerateNewId();
            eff.Schema = Enum.Parse<EffT>(schemaName).CreateSchema();
            eff.modelUid = (IID) (int) Enum.Parse<EffT>(schemaName);
            await _effects.CreateAsync(eff);
            return eff;
        }

        //[Authorize(Roles = nameof(AccountType.Admin))]
        //[HttpPost("{id}/newEffect")]
        //public async Task<ActionResult<T>> AddEffect([FromRoute] ObjectId id, [FromQuery] string schemaName)
        //{
        //    var model = await _containers.GetOneAsync(id);
        //    if (model is null)
        //        return NoContent();

        //    var eff = await newEffect(schemaName);
        //    model.EffectIds.Add(eff.entityUid);
        //    var result = await _containers.UpdateAsync(model.entityUid, model);
        //    if (result.MatchedCount > 0)
        //        return Ok(model);
        //    return BadRequest();
        //}

        //[Authorize(Roles = nameof(AccountType.Admin))]
        //[HttpPost("{id}/pasteEffect")]
        //public async Task<ActionResult<IEffect>> pasteNew([FromRoute] ObjectId id, [FromBody] IEffect effect)
        //{
        //    var parent = await _containers.GetOneAsync(id);
        //    if (parent is null)
        //        return NoContent();

        //    // create new effect
        //    effect.entityUid = Eevee.RegisterIIDTemporary();
        //    await _effects.CreateAsync(effect);

        //    // add to parent
        //    parent.EffectIds.Add(effect.entityUid);
        //    var result = await _containers.UpdateAsync(id, parent);

        //    if (result.ModifiedCount > 0)
        //        return Ok(parent);
        //    return BadRequest();
        //}


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}/addEffect/{addid}")]
        public async Task<ActionResult<IEffect>> add([FromRoute] ObjectId id, [FromRoute] ObjectId addid)
        {
            // can't add an effect to itself
            if (addid.Equals(id))
                return BadRequest();

            var model = await _effects.GetOneAsync(addid);
            if (model is null)
                return NoContent();

            var arrayname = nameof(IEffect.EffectIds) + "._v";

            // add
            var filter = _containers.filterId(id);
            var updater = Builders<T>.Update.Push(arrayname, addid);
            var result = await _containers.UpdateAsync(filter, updater);

            if (result?.ModifiedCount > 0)
                return Ok(model);
            return BadRequest();
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("removeEffect/{id}")]
        public async Task<ActionResult<IEffect>> remove([FromRoute] ObjectId id)
        {
            var model = await _effects.GetOneAsync(id);
            if (model is null)
                return NoContent();

            var arrayname = nameof(IEffect.EffectIds) + "._v";

            // remove
            var filter = Builders<T>.Filter.Eq(arrayname, id);
            var updater = Builders<T>.Update.Pull(arrayname, id);
            var result = await _containers.UpdateAsync(filter, updater);

            if (result?.ModifiedCount > 0)
                return Ok(model);
            return BadRequest();
        }

    }
}
