using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.models;
using System.Data;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Models + "trigger")]
    public class TriggerModelController : ControllerBase
    {
        private readonly CollectionService<IEffect> _effects;
        private readonly CollectionService<IStats> _stats;
        private readonly StringService _strings;
        private readonly IDCounterService _ids;
        private readonly MongoFederationService _federation;

        public TriggerModelController(MongoModelsDbService db, StringService strings, IDCounterService ids, MongoFederationService federation)
        {
            _effects = db.GetMongoService<IEffect>();
            _stats = db.GetMongoService<IStats>();
            _strings = strings;
            _ids = ids;
            _federation = federation;
        }


        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPost("trigger")]
        public ActionResult<ITriggerModel> CreateTrigger([FromRoute] IID triggerTypeId) // TriggerType triggerType) //
        {
            var type = TriggerType.get(triggerTypeId);
            if (type == null)
                return null;

            //var trigger = new TriggerModel();
            var trigger = type.createInstance();
            return Ok(trigger);
        }


    }
}
