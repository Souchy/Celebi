using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.spark.services.models;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "effects")] 
    public class EffectController : ControllerBase
    {
        private readonly EffectService _effectService;

        public EffectController(EffectService service) => _effectService = service;

        [HttpGet("all")]
        public async Task<List<IEffect>> GetAll() => await _effectService.GetAsync();

        [HttpGet("effect/{id}")]
        public async Task<ActionResult<IEffect>> Get(string id)
        {
            IEffect? effect = await _effectService.GetAsync(id);
            if (effect is null)
                return NotFound();
            return Ok(effect);
        }

        [Authorize]
        [HttpPost("effect")]
        public async Task<IActionResult> Post(Effect newEffect)
        {
            await _effectService.CreateAsync(newEffect);
            return CreatedAtAction(nameof(Get), new { id = newEffect.entityUid }, newEffect);
        }

        [Authorize]
        [HttpPut("effect/{id}")]
        public async Task<ActionResult<ReplaceOneResult>> Update(string id, Effect updatedEffect)
        {
            var effect = await _effectService.GetAsync(id);
            if (effect is null) 
                return NotFound();
            updatedEffect.entityUid = effect.entityUid;
            var result = await _effectService.UpdateAsync(id, updatedEffect);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("effect/{id}")]
        public async Task<ActionResult<DeleteResult>> Delete(string id)
        {
            var effect = await _effectService.GetAsync(id);
            if (effect is null) 
                return NotFound();
            var result = await _effectService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
