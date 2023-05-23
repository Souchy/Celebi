using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "creature/skin")]
    public class CreatureSkinController : ControllerBase
    {
        private readonly CollectionService<ICreatureSkin> _skins;
        private readonly CollectionService<IStringEntity> _strings;
        private readonly IDCounterService _ids;

        public CreatureSkinController(CollectionService<ICreatureSkin> skins, CollectionService<IStringEntity> strings, IDCounterService ids)
        {
            _skins = skins;
            _strings = strings;
            _ids = ids;
        }

        [HttpGet("all")]
        public async Task<List<ICreatureSkin>> GetAll()
            => await _skins.GetAsync();
        [HttpGet("{id}")]
        public async Task<ICreatureSkin?> GetCreatureSkin([FromRoute] ObjectId id)
            => await _skins.GetOneAsync(id);
        [HttpGet("{ids}")]
        public async Task<List<ICreatureSkin>> GetCreatureSkins([FromQuery] IEnumerable<ObjectId> skinIds)
            => await _skins.GetInIdsAsync(skinIds);

        [HttpPost]
        public async Task<ActionResult<ICreatureSkin>> PostNew()
        {
            var skin = await Factories.newCreatureSkin(_ids);
            return Ok(skin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ICreatureSkin>> Update([FromRoute] ObjectId id, [FromBody] ICreatureSkin skin)
        {
            var model = await _skins.GetOneAsync(id);
            if (model is null)
                return NotFound();
            await _skins.UpdateAsync(id, skin);
            return Ok(skin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] ObjectId id)
        {
            var model = await _skins.GetOneAsync(id);
            if (model is null)
                return NotFound();
            var result = await _skins.RemoveAsync(id);
            await _strings.RemoveAsync(model.nameId);
            await _strings.RemoveAsync(model.descriptionId);
            // remove from creature?
            return Ok(result);
        }

    }
}
