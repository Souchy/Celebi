using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "condition")]
    public class ConditionModelController : ControllerBase
    {
        private readonly CollectionService<IEffect> _effects;

        public ConditionModelController(MongoModelsDbService db)
        {
            _effects = db.GetMongoService<IEffect>();
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("new")]
        public ActionResult<ICondition> CreateCondition([FromQuery] string schemaName)
        {
            var schemaType = ConditionType.getByName(schemaName);
            if (schemaType == null)
                return BadRequest();
            var condition = schemaType.createInstance();
            return Ok(condition);
        }


    }
}
