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
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "creature")] 
    public class CreatureModelController : ControllerBase
    {
        private readonly CollectionService<ICreatureModel> _creatureModels;
        private readonly CollectionService<IStats> _stats;
        private readonly CollectionService<ICreatureSkin> _skins;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;
        private readonly MongoFederationService _federation;

        public CreatureModelController(MongoModelsDbService db, StringService strings, IDCounterService ids, MongoFederationService  federation)
        {
            _creatureModels = db.GetMongoService<ICreatureModel>();
            _stats = db.GetMongoService<IStats>();
            _skins = db.GetMongoService<ICreatureSkin>();   
            _strings = strings;
            _ids = ids;
            _federation = federation;
        }

        [HttpGet("all")]
        public async Task<List<ICreatureModel>> GetAll() => await _creatureModels.GetAsync();

        [HttpGet("filtered")]
        public async Task<List<ICreatureModel>> GetFiltered(FilterDefinition<ICreatureModel> filter) 
            => await _creatureModels.GetAsync(filter);

        [HttpGet("byString/{str}")]
        public async Task<List<ICreatureModel>> GetByString(string str) => await _federation.FindCreaturesByString(str);


        [HttpGet("{id}")]
        public async Task<ActionResult<ICreatureModel>> Get([FromRoute] CreatureIID id)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id.value);
            var model = await _creatureModels.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            return Ok(model);
        }

        //[Authorize]
        //[HttpPost("")]
        //public async Task<ActionResult<ICreatureModel>> Post(CreatureModel newCreatureModel)
        //{
        //    await _creatureModels.CreateAsync(newCreatureModel);
        //    return CreatedAtAction(nameof(Get), new { id = newCreatureModel.entityUid }, newCreatureModel);
        //}

        [HttpPost("new")]
        public async Task<ActionResult<ICreatureModel>> PostNew()
        {
            var model = await Factories.newCreatureModel(_ids);

            await _creatureModels.CreateAsync(model.crea);
            await _strings.CreateAsync(model.name);
            await _strings.CreateAsync(model.desc);
            await _stats.CreateAsync(model.stats);

            await _skins.CreateAsync(model.skin.skin);
            await _strings.CreateAsync(model.skin.name);
            await _strings.CreateAsync(model.skin.desc);

            return CreatedAtAction(nameof(Get), new { id = model.Item1.entityUid }, model.Item1);
        }
        

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] CreatureIID id, [FromBody] CreatureModel updateModel)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id.value);
            var model = await _creatureModels.GetOneAsync(filter);
            if (model is null) 
                return NotFound();
            updateModel.entityUid = model.entityUid;
            updateModel.modelUid = model.modelUid;
            var result = await _creatureModels.UpdateAsync(filter, updateModel);
            return Ok(result);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] CreatureIID id)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id.value);
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

        //[Authorize]
        [HttpPut("{id}/spells")]
        public async Task<ActionResult<UpdateResult>> UpdateSpell([FromRoute] CreatureIID id, [FromBody] string[] spellIds)
        {
            var filter = Builders<ICreatureModel>.Filter.Eq(nameof(ICreatureModel.modelUid), id.value);
            var model = await _creatureModels.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            //foreach (var s in spellIds)
            //    model.baseSpells.Add(new ObjectId(s));
            var objectIds = spellIds.Distinct().Select(s => new ObjectId(s));
            var set = new EntitySet<ObjectId>(objectIds);
            var update = Builders<ICreatureModel>.Update.Set(nameof(ICreatureModel.spellIds), set);
            var result = await _creatureModels.Collection.UpdateOneAsync(filter, update);
            return Ok(result);
        }

    }
}
