﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.util;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "spell/skin")]
    public class SpellSkinController : ControllerBase
    {
        private readonly CollectionService<ISpellSkin> _skins;
        //private readonly CollectionService<IStringEntity> _strings;
        private readonly IDCounterService _ids;

        public SpellSkinController(CollectionService<ISpellSkin> skins, CollectionService<IStringEntity> strings, IDCounterService ids)
        {
            _skins = skins;
            //_strings = strings;
            _ids = ids;
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

        [HttpPost]
        public async Task<ActionResult<ISpellSkin>> PostNew()
        {
            var skin = await Factories.newCreatureSkin(_ids);
            return Ok(skin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ISpellSkin>> Update([FromRoute] ObjectId id, [FromBody] ISpellSkin skin)
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
            // remove from creature skins?
            return Ok(result);
        }

    }
}
