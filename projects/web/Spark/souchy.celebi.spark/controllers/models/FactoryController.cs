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
        public ActionResult<ITrigger> CreateTrigger()
        {
            var trigger = new Trigger();
            return Ok(trigger);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("condition")]
        public async Task<ActionResult<ICondition>> CreateCondition(
            //[FromQuery] ConditionType conditionType
            )
        {
            var condition = new CreatureStatsCondition();
            return Ok(condition);
        }


    }
}
