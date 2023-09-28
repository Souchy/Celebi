using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;
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
        public ActionResult<ITriggerModel> CreateTrigger([FromQuery] string schemaName)
        {
            var schemaType = TriggerType.getByName(schemaName);
            if (schemaType == null)
                return null;
            var triggerModel = TriggerModel.Create();
            triggerModel.schema = schemaType.createInstance();
            return Ok(triggerModel);
        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpDelete("{effectId}/{triggerId}")]
        public async Task RemoveTrigger([FromRoute] ObjectId effectId, [FromRoute] ObjectId triggerId)
        {

        }

        [Authorize(Roles = nameof(AccountType.Admin))]
        [HttpPut("{effectId}/{triggerId}/schema")]
        public async Task<ActionResult<ITriggerModel>> ChangeSchema([FromRoute] ObjectId effectId, [FromRoute] ObjectId triggerId, [FromQuery] string schemaName)
        {
            var effect = await _effects.GetOneAsync(effectId);
            if (effect is null)
                return NoContent();

            //var model = effect.Triggers.Values.First(t => t.en)

            //effect.Schema = Enum.Parse<EffT>(schemaName).CreateSchema();
            //effect.modelUid = (IID) (int) Enum.Parse<EffT>(schemaName);
            //var result = await _effects.UpdateAsync(effect.entityUid, effect);
            //if (result.ModifiedCount > 0)
            //    return Ok(effect);
            return BadRequest();
        }

    }
}
