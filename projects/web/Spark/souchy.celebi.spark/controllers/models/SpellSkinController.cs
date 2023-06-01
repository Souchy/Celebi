using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.util;
using Microsoft.AspNetCore.Authorization;
using souchy.celebi.spark.models;
using System.Data;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "spell/skin")]
    public class SpellSkinController : ControllerBase
    {
        private readonly CollectionService<ISpellSkin> _skins;

        public SpellSkinController(MongoModelsDbService db)
        {
            _skins = db.GetMongoService<ISpellSkin>();
        }

        [HttpGet("all")]
        public async Task<List<ISpellSkin>> GetAll()
            => await _skins.GetAsync();
        [HttpGet("{id}")]
        public async Task<ISpellSkin?> GetSkin([FromRoute] ObjectId id)
            => await _skins.GetOneAsync(id);
        [HttpGet("{ids}")]
        public async Task<List<ISpellSkin>> GetSkins([FromQuery] IEnumerable<ObjectId> skinIds)
            => await _skins.GetInIdsAsync(skinIds);

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost]
        public ActionResult<ISpellSkin> PostNew()
        {
            var skin = Factories.newSpellSkin();
            return Ok(skin);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ISpellSkin>> Update([FromRoute] ObjectId id, [FromBody] ISpellSkin skin)
        {
            var model = await _skins.GetOneAsync(id);
            if (model is null)
                return NotFound();
            await _skins.UpdateAsync(id, skin);
            return Ok(skin);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete([FromRoute] ObjectId id)
        {
            var model = await _skins.GetOneAsync(id);
            if (model is null)
                return NotFound();
            var result = await _skins.RemoveAsync(id);
            // remove from creature skins?
            return Ok(result);
        }

    }
}
