using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "status")]
    public class StatusModelController : EffectContainerControllerBase<IStatusModel>
    {
        private readonly CollectionService<IStatusModel> statuses;
        private readonly CollectionService<IStatusSkin> _skins;
        private readonly CollectionService<IStats> _stats;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;
        private readonly MongoFederationService _federation;
        public StatusModelController(MongoModelsDbService db, StringService strings, IDCounterService ids, MongoFederationService federation) : base(db)
        {
            statuses = db.GetMongoService<IStatusModel>();
            _skins = db.GetMongoService<IStatusSkin>();
            _stats = db.GetMongoService<IStats>();
            _strings = strings;
            _ids = ids;
            _federation = federation;
        }

        [HttpGet("all")]
        public async Task<List<IStatusModel>> GetAll() => await statuses.GetAsync();

        [HttpGet("filtered")]
        public async Task<List<IStatusModel>> GetFiltered(FilterDefinition<IStatusModel> filter)
            => await statuses.GetAsync(filter);

        [HttpGet("byString/{str}")]
        public async Task<List<IStatusModel>> GetByString(string str) => await _federation.FindStatusesByString(str);


        [HttpGet("{id}")]
        public async Task<ActionResult<IStatusModel>> Get(ObjectId id)
        {
            IStatusModel? creatureModel = await statuses.GetOneAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("new")]
        public async Task<ActionResult<IStatusModel>> PostNew()
        {
            var model = await Factories.newStatusModel(_ids);

            await statuses.CreateAsync(model.status);
            await _strings.CreateAsync(model.name);
            await _strings.CreateAsync(model.desc);
            await _stats.CreateAsync(model.stats);
            await _skins.CreateAsync(model.skin);

            return CreatedAtAction(nameof(Get), new { id = model.status.entityUid }, model.status);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<IStatusModel>> Update([FromRoute] ObjectId id, [FromBody] StatusModel updatedModel)
        {
            var oldModel = await statuses.GetOneAsync(id);
            if (oldModel is null)
                return NotFound();
            updatedModel.entityUid = oldModel.entityUid;
            var result = await statuses.UpdateAsync(id, updatedModel);
            if (result.MatchedCount > 0) return Ok(updatedModel);
            else return Ok(oldModel);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(ObjectId id)
        {
            var crea = await statuses.GetOneAsync(id);
            if (crea is null)
                return NotFound();
            var result = await statuses.RemoveAsync(id);
            return Ok(result);
        }
    }
}
