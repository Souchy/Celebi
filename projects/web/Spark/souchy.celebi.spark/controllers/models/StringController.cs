using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "strings")] 
    public class StringController : ControllerBase
    {
        private readonly StringService _stringService;

        public StringController(StringService service) => _stringService = service;

        [HttpGet("all")]
        public async Task<List<IStringEntity>> GetAll() => await _stringService.GetAsync();
        [HttpGet("filtered")]
        public async Task<List<IStringEntity>> GetAll(FilterDefinition<IStringEntity>? filter = null) => await _stringService.GetAsync(filter);

        [HttpGet("string/{id}")]
        public async Task<ActionResult<IStringEntity>> Get(string id)
        {
            IStringEntity? str = await _stringService.GetAsync(id);
            if (str is null)
                return NotFound();
            return Ok(str);
        }

        [Authorize]
        [HttpPost("string")]
        public async Task<IActionResult> Post(StringEntity newStringEntity)
        {
            await _stringService.CreateAsync(newStringEntity);
            return CreatedAtAction(nameof(Get), new { id = newStringEntity.entityUid }, newStringEntity);
        }

        [Authorize]
        [HttpPut("string/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(string id, StringEntity updatedStringEntity)
        {
            var str = await _stringService.GetAsync(id);
            if (str is null) 
                return NotFound();
            updatedStringEntity.entityUid = str.entityUid;
            var result = await _stringService.UpdateAsync(id, updatedStringEntity);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("string/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(string id)
        {
            var str = await _stringService.GetAsync(id);
            if (str is null) 
                return NotFound();
            var result = await _stringService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
