using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "string")] 
    public class StringController : ControllerBase
    {
        private readonly StringService _stringService;
        public StringController(StringService service) => _stringService = service;

        [HttpGet("all")]
        public async Task<List<IStringEntity>> GetAll([FromQuery] I18NType lang) 
            => await _stringService.GetAsync(lang);
        [HttpGet("filtered")]
        public async Task<List<IStringEntity>> GetAll([FromQuery] I18NType lang, FilterDefinition<IStringEntity> filter = null) 
            => await _stringService.GetAsync(lang, filter);
        [HttpGet("{id}")]
        public async Task<ActionResult<IStringEntity>> Get([FromQuery] I18NType lang, [FromRoute] ObjectId id)
        {
            IStringEntity? str = await _stringService.GetAsync(lang, id);
            if (str is null)
                return NotFound();
            return Ok(str);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update([FromRoute] ObjectId id, [FromQuery] I18NType lang, [FromBody] StringEntity updatedStringEntity)
        {
            var str = await _stringService.GetAsync(lang, id);
            if (str is null) 
                return NotFound();
            updatedStringEntity.entityUid = str.entityUid;
            var result = await _stringService.UpdateAsync(lang, id, updatedStringEntity);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new string entity in all languages
        /// </summary>
        //[Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Create(StringEntity newStringEntity)
        {
            await _stringService.CreateAsync(newStringEntity);
            return CreatedAtAction(nameof(Get), new { id = newStringEntity.entityUid }, newStringEntity);
        }
        /// <summary>
        /// Remove a string entity from all languages
        /// </summary>
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(ObjectId id)
        {
            var str = await _stringService.GetAsync(I18NType.fr, id);
            if (str is null) 
                return NotFound();
            var result = await _stringService.RemoveAsync(id);
            return Ok(result);
        }

    }
}
