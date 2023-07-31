using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.models;
using souchy.celebi.spark.models.aggregations;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util;
using System.Collections.Generic;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "spell")]
    public class SpellModelController : EffectContainerControllerBase<ISpellModel>
    {
        private readonly CollectionService<ISpellModel> spells;
        private readonly CollectionService<IStats> _stats;
        private readonly CollectionService<ISpellSkin> _skins;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;
        private readonly MongoFederationService _federation;

        public SpellModelController(MongoModelsDbService db, StringService strings, IDCounterService ids, MongoFederationService federation) : base(db)
        {
            spells = db.GetMongoService<ISpellModel>();
            _stats = db.GetMongoService<IStats>();
            _skins = db.GetMongoService<ISpellSkin>();
            _strings = strings;
            _ids = ids;
            _federation = federation;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ISpellModel>>> GetAll()
        {
            var list = await spells.GetAsync();
            return Ok(list);
        }

        // TODO maybe, but we need the Atlas Federation for aggregation. More important thing to fix now is getting models with the new stats and pushing the build to DeathShadows
        // These would make it faster to request all the info about the model list and the individual model, 
        // instead of requesting the list of ids and then requesting the names/desc/stats/etc individually for each model
        // It could disrupt my flow of data when updating values though
        //[HttpGet("aggregation/list")]
        //public async Task<ActionResult<List<SpellModelTextAggregation>>> GetListAggreation([FromQuery] Optional<List<ObjectId>> list)
        //{
        //    return Ok(await spells.GetInIdsAsync(list));
        //}
        //[HttpGet("aggregation/{id}")]
        //public async Task<ActionResult<SpellModelAggregation>> GetAggregation([FromRoute] SpellIID id)
        //{
        //    SpellModelAggregation? model = await spells.GetOneAsync(id);
        //    if (model is null)
        //        return NoContent();
        //    return Ok(model);
        //}

        [HttpGet("list")]
        public async Task<ActionResult<List<ISpellModel>>> GetList([FromQuery] List<ObjectId> list)
            => Ok(await spells.GetInIdsAsync(list));

        [HttpGet("filtered")]
        public async Task<ActionResult<List<ISpellModel>>> GetFiltered(FilterDefinition<ISpellModel> filter)
            => Ok(await spells.GetAsync(filter));

        [HttpGet("byString/{str}")]
        public async Task<List<ISpellModel>> GetByString(string str) => await _federation.FindSpellsByString(str);


        [HttpGet("{id}")]
        public async Task<ActionResult<ISpellModel>> Get([FromRoute] SpellIID id)
        {
            ISpellModel? model = await spells.GetOneAsync(id);
            if (model is null)
                return NoContent();
            return Ok(model);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("new")]
        public async Task<ActionResult<ISpellModel>> PostNew()
        {
            var model = await Factories.newSpellModel(_ids);

            await spells.CreateAsync(model.spell);
            await _strings.CreateAsync(model.name);
            await _strings.CreateAsync(model.desc);
            await _stats.CreateAsync(model.stats);
            await _skins.CreateAsync(model.skin);

            return CreatedAtAction(nameof(Get), new { id = model.spell.entityUid }, model.spell);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ISpellModel>> Update([FromRoute] SpellIID id, [FromBody] SpellModel updatedModel)
        {
            var model = await spells.GetOneAsync(id);
            if (model is null)
                return NoContent();
            updatedModel.entityUid = model.entityUid;
            var result = await spells.UpdateAsync(id, updatedModel);
            if(result.MatchedCount > 0) 
                return Ok(updatedModel);
            return Ok(model);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] SpellIID id)
        {
            var model = await spells.GetOneAsync(id);
            if (model is null)
                return NoContent();
            var result = await spells.RemoveAsync(id);
            await _strings.RemoveAsync(model.nameId);
            await _strings.RemoveAsync(model.descriptionId);
            await _stats.RemoveAsync(model.statsId);
            if(result.DeletedCount> 0)
                return Ok(result);
            return BadRequest();
        }


    }
}
