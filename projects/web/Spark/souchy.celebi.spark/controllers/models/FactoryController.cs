using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.impl.shared.conditions.creature;
using souchy.celebi.eevee.impl.shared.triggers;
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
        public ActionResult<ITriggerModel> CreateTrigger() 
        {
            var trigger = new TriggerModel();
            return Ok(trigger);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("condition")]
        public async Task<ActionResult<ICondition>> CreateCondition() //[FromQuery] ConditionType conditionType) //[FromRoute] ObjectId id)
        {
            var condition = new CreatureStatsCondition();
            return Ok(condition);
        }


    }
}
