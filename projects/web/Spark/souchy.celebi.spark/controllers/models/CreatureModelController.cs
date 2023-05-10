using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.factories;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;
namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "creatures")] 
    public class CreatureModelController : ControllerBase
    {
        private readonly CollectionService<ICreatureModel> _creatureModels;
        private readonly CollectionService<IStats> _stats;
        private readonly CollectionService<ICreatureSkin> _skins;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;

        public CreatureModelController(MongoModelsDbService db, StringService strings, IDCounterService ids)
        {
            _creatureModels = db.GetMongoService<ICreatureModel>();
            _stats = db.GetMongoService<IStats>();
            _skins = db.GetMongoService<ICreatureSkin>();   
            _strings = strings;
            _ids = ids;
        }

        [HttpGet("all")]
        public async Task<List<ICreatureModel>> GetAll() => await _creatureModels.GetAsync();

        [HttpGet("filtered")]
        public async Task<List<ICreatureModel>> GetFiltered(FilterDefinition<ICreatureModel> filter) 
            => await _creatureModels.GetAsync(filter);

        [HttpGet("creature/{id}")]
        public async Task<ActionResult<ICreatureModel>> Get([FromRoute] IID id)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id);
            var model = await _creatureModels.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            return Ok(model);
        }

        //[Authorize]
        [HttpPost("creature")]
        public async Task<ActionResult<ICreatureModel>> Post(CreatureModel newCreatureModel)
        {
            await _creatureModels.CreateAsync(newCreatureModel);
            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }

        [HttpPost("new")]
        public async Task<ActionResult<ICreatureModel>> PostNew()
        {
            var newCreatureModel = Factories.newCreatureModel();
            newCreatureModel.GetBaseStats().Add(StatSimple.Create(Resource.Life.ID, 50));

            newCreatureModel.modelUid = await _ids.GetID<ICreatureModel>();
            newCreatureModel.GetName().modelUid = await _ids.GetID<IStringEntity>();
            newCreatureModel.GetDescription().modelUid = await _ids.GetID<IStringEntity>();

            var skin = newCreatureModel.GetBaseSkin();
            skin.GetName().modelUid = await _ids.GetID<IStringEntity>();
            skin.GetDescription().modelUid = await _ids.GetID<IStringEntity>();

            await _stats.CreateAsync(newCreatureModel.GetBaseStats());
            await _stats.CreateAsync(newCreatureModel.GetGrowthStats());
            await _strings.CreateAsync(newCreatureModel.GetName());
            await _strings.CreateAsync(newCreatureModel.GetDescription());
            await _creatureModels.CreateAsync(newCreatureModel);

            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }
        

        [Authorize]
        [HttpPut("creature/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] string id, [FromBody] CreatureModel updateModel)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id);
            var model = await _creatureModels.GetOneAsync(filter);
            if (model is null) 
                return NotFound();
            updateModel.entityUid = model.entityUid;
            updateModel.modelUid = model.modelUid;
            var result = await _creatureModels.UpdateAsync(filter, updateModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("creature/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] IID id)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id);
            var model = await _creatureModels.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            var result = await _creatureModels.RemoveAsync(filter);
            await _strings.RemoveAsync(model.nameId);
            await _strings.RemoveAsync(model.descriptionId);
            foreach(var skin in model.GetSkins())
            {
                await _skins.RemoveAsync(skin.entityUid);
                await _strings.RemoveAsync(skin.nameId);
                await _strings.RemoveAsync(skin.descriptionId);
            }
            return Ok(result);
        }
    }
}
