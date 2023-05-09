using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.factories;
using souchy.celebi.eevee.impl.shared;
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
        private readonly StringService _strings;
        private readonly IDCounterService _ids;

        public CreatureModelController(MongoModelsDbService db, StringService strings, IDCounterService ids)
        {
            _creatureModels = db.GetMongoService<ICreatureModel>();
            _stats = db.GetMongoService<IStats>();
            _strings = strings;
            _ids = ids;
        }

        [HttpGet("all")]
        public async Task<List<ICreatureModel>> GetAll() => await _creatureModels.GetAsync();

        [HttpGet("filtered")]
        public async Task<List<ICreatureModel>> GetFiltered(FilterDefinition<ICreatureModel> filter) 
            => await _creatureModels.GetFilteredAsync(filter);

        [HttpGet("creature/{id}")]
        public async Task<ActionResult<ICreatureModel>> Get([FromRoute] ObjectId id)
        {
            var creatureModel = await _creatureModels.GetAsync(id);
            if (creatureModel is null)
                return NotFound();
            return Ok(creatureModel);
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
            //var resources = Resource.values.Values;
            //var characs = Enumerable.OfType<CharacteristicType>(resources);
            var newCreatureModel = Factories.newCreatureModel();

            //newCreatureModel.entityUid = ObjectId.GenerateNewId().ToString();
            newCreatureModel.modelUid = await _ids.GetID<ICreatureModel>();
            newCreatureModel.GetName().modelUid = await _ids.GetID<IStringEntity>();
            newCreatureModel.GetDescription().modelUid = await _ids.GetID<IStringEntity>();

            var skin = newCreatureModel.GetBaseSkin();
            skin.GetName().modelUid = await _ids.GetID<IStringEntity>();
            skin.GetDescription().modelUid = await _ids.GetID<IStringEntity>();
            //newCreatureModel.nameId = await _ids.GetID<IStringEntity>();
            //newCreatureModel.descriptionId = await _ids.GetID<IStringEntity>();
            //// object ids for stats and effects
            //newCreatureModel.GetBaseStats().entityUid = (IID)ObjectId.GenerateNewId().ToString();
            //newCreatureModel.GetGrowthStats().entityUid = (IID)ObjectId.GenerateNewId().ToString();
            //newCreatureModel.baseStats = newCreatureModel.GetBaseStats().entityUid;
            //newCreatureModel.growthStats = newCreatureModel.GetGrowthStats().entityUid;

            await _stats.CreateAsync(newCreatureModel.GetBaseStats());
            await _stats.CreateAsync(newCreatureModel.GetGrowthStats());
            await _strings.CreateAsync(newCreatureModel.GetName());
            await _strings.CreateAsync(newCreatureModel.GetDescription());
            await _creatureModels.CreateAsync(newCreatureModel);

            return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        }
        

        [Authorize]
        [HttpPut("creature/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] ObjectId id, [FromBody] CreatureModel updatedCreatureModel)
        {
            var crea = await _creatureModels.GetAsync(id);
            if (crea is null) 
                return NotFound();
            updatedCreatureModel.entityUid = crea.entityUid;
            var result = await _creatureModels.UpdateAsync(id, updatedCreatureModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("creature/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] ObjectId id)
        {
            var crea = await _creatureModels.GetAsync(id);
            if (crea is null) 
                return NotFound();
            var result = await _creatureModels.RemoveAsync(id);
            return Ok(result);
        }
    }
}
