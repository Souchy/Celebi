using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "enums")]
    public class EnumsController : ControllerBase
    {
        [HttpGet("charac/characteristicCategory")]
        public CharacteristicCategory[] GetCharacteristicCategories() => Enum.GetValues<CharacteristicCategory>();
        [HttpGet("charac/resourceType")]
        public ResourceProperty[] GetResourceType() => Enum.GetValues<ResourceProperty>();
        [HttpGet("charac/resourceProperty")]
        public ResourceEnum[] GetResourceProperty() => Enum.GetValues<ResourceEnum>();

        [HttpGet("actorType")]
        public ActorType[] GetActorTypes() => Enum.GetValues<ActorType>();
        [HttpGet("boardTargetType")]
        public BoardTargetType[] GetBoardTargetType() => Enum.GetValues<BoardTargetType>();
        [HttpGet("moveType")]
        public MoveType[] GetMoveType() => Enum.GetValues<MoveType>();
        [HttpGet("teamRelationType")]
        public TeamRelationType[] GetTeamRelationType() => Enum.GetValues<TeamRelationType>();
        [HttpGet("zoneType")]
        public ZoneType[] GetZoneType() => Enum.GetValues<ZoneType>();
        [HttpGet("towerDirectionType")]
        public TowerDirectionType[] GetTowerDirectionType() => Enum.GetValues<TowerDirectionType>();
        [HttpGet("direction8Type")]
        public Direction8Type[] GetDirection8Type() => Enum.GetValues<Direction8Type>();
        [HttpGet("direction9Type")]
        public Direction9Type[] GetDirection9Type() => Enum.GetValues<Direction9Type>();

        [HttpGet("statusPriorityType")]
        public StatusPriorityType[] GetStatusPriorityType() => Enum.GetValues<StatusPriorityType>();
        [HttpGet("statusMergeStrategy")]
        public StatusMergeStrategy[] GetStatusMergeStrategy() => Enum.GetValues<StatusMergeStrategy>();
        [HttpGet("statusUnbewitchStrategy")]
        public StatusUnbewitchStrategy[] GetStatusUnbewitchStrategy() => Enum.GetValues<StatusUnbewitchStrategy>();

        [HttpGet("effectType")]
        public EffT[] GetEffectType() => Enum.GetValues<EffT>();

        [HttpGet("triggerType")]
        public TriggerType[] GetTriggerType() => TriggerType.values();
        [HttpGet("conditionType")]
        public ConditionType[] GetConditionType() => ConditionType.values();

        // Schemas
        [HttpGet("schemas/effects")]
        public ActionResult<IEnumerable<SchemaDescription>> GetEffectSchemas()
        {
            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffectSchema)));
            var descriptions = schemas.Select(t => SchemaDescription.GetSchemaDescription(t));
            return Ok(descriptions);
        }
        [HttpGet("schemas/effect/{name}")]
        public ActionResult<SchemaDescription> GetEffectSchema(string name)
        {
            var type = typeof(IEntity).Assembly
                .GetTypes().FirstOrDefault(t => t.Name == name);
            if (type == null) return NotFound();
            return Ok(SchemaDescription.GetSchemaDescription(type));
        }

        [HttpGet("schemas/triggers")]
        public ActionResult<IEnumerable<SchemaDescription>> GetTriggerSchemas()
        {
            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(ITriggerSchema)));
            var descriptions = schemas.Select(t => SchemaDescription.GetSchemaDescription(t));
            return Ok(descriptions);
        }

        [HttpGet("schemas/conditions")]
        public ActionResult<IEnumerable<SchemaDescription>> GetConditionSchemas()
        {
            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(ICondition)));
            var descriptions = schemas.Select(t => SchemaDescription.GetSchemaDescription(t));
            return Ok(descriptions);
        }

    }
}
