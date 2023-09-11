using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.services;
using MongoDB.Driver;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared;
using Microsoft.AspNetCore.Authorization;
using souchy.celebi.spark.models;
using System.Data;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.spark.util;
using souchy.celebi.eevee.impl.shared.skins;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "map")]
    public class MapModelController : ControllerBase
    {
        private readonly CollectionService<IMap> _maps;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;
        private readonly MongoFederationService _federation;

        public MapModelController(MongoModelsDbService db, StringService strings, IDCounterService ids, MongoFederationService federation)
        {
            _maps = db.GetMongoService<IMap>();
            _strings = strings;
            _ids = ids;
            _federation = federation;
        }

        [HttpGet("all")]
        public async Task<List<IMap>> GetAll() => await _maps.GetAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<IMap>> Get([FromRoute] IID id)
        {
            var filter = Builders<IMap>.Filter.Eq(nameof(IMap.modelUid), id.value);
            var model = await _maps.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            return Ok(model);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("new")]
        public async Task<ActionResult<IMap>> PostNew()
        {
            //TODO var model = await Factories.newMap(_ids);
            var model = (map: Map.Create(), name: StringEntity.Create(), skin: MapSkin.Create());


            await _maps.CreateAsync(model.map);
            await _strings.CreateAsync(model.name);
            //await _strings.CreateAsync(model.desc);

            //await _strings.CreateAsync(model.skin.name);
            //await _strings.CreateAsync(model.skin.desc);

            return CreatedAtAction(nameof(Get), new { id = model.map.entityUid }, model.map);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] IID id, [FromBody] Map updateModel)
        {
            var filter = Builders<IMap>.Filter.Eq(nameof(IMap.modelUid), id.value);
            var model = await _maps.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            updateModel.entityUid = model.entityUid;
            updateModel.modelUid = model.modelUid;
            var result = await _maps.UpdateAsync(filter, updateModel);
            return Ok(result);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] IID id)
        {
            var filter = Builders<IMap>.Filter.Eq(nameof(IMap.modelUid), id.value);
            var model = await _maps.GetOneAsync(filter);
            if (model is null)
                return NotFound();
            var result = await _maps.RemoveAsync(filter);
            await _strings.RemoveAsync(model.nameId);
            //await _strings.RemoveAsync(model.descriptionId);
            //foreach (var skin in model.GetSkins())
            //{
            //    await _skins.RemoveAsync(skin.entityUid);
            //    await _strings.RemoveAsync(skin.nameId);
            //    await _strings.RemoveAsync(skin.descriptionId);
            //}
            return Ok(result);
        }
    }
}
