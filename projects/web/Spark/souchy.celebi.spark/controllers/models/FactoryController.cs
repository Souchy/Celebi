using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.spark.models;
using System.Data;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "factory")]
    public class FactoryController : ControllerBase
    {


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("trigger")]
        //[HttpPost("{id}/trigger")]
        public async Task<ActionResult<ITrigger>> CreateTrigger() //[FromRoute] ObjectId id)
        {
            //var model = await _effects.GetOneAsync(id);
            //if (model is null)
            //    return NotFound();

            var trigger = new Trigger();
            //model.Triggers.Add(trigger);

            //var result = await _effects.UpdateAsync(model.entityUid, model);
            //return Ok(model);
            return Ok(trigger);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("condition")]
        //[HttpPost("{id}/trigger")]
        public async Task<ActionResult<ICondition>> CreateCondition([FromQuery] ConditionType conditionType) //[FromRoute] ObjectId id)
        {
            //var model = await _effects.GetOneAsync(id);
            //if (model is null)
            //    return NotFound();

            var trigger = new Trigger();
            //model.Triggers.Add(trigger);

            //var result = await _effects.UpdateAsync(model.entityUid, model);
            //return Ok(model);
            return Ok(trigger);
        }


    }
}
