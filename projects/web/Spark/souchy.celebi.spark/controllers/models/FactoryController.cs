using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.util;
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
        public ActionResult<ITriggerModel> CreateTrigger([FromRoute] TriggerType triggerType) 
        {
            var trigger = new TriggerModel();
            return Ok(trigger);
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("condition")]
        public ActionResult<ICondition> CreateCondition([FromRoute] IID conditionType) //[FromQuery] ConditionType conditionType) //[FromRoute] ObjectId id)
        {
            //var condition = new CreatureStatsCondition();
            var condition = ConditionType.get(conditionType).createInstance();
            return Ok(condition);
        }


    }
}
