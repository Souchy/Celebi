using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
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
        [HttpGet("triggerType")]
        public TriggerType[] GetTriggerType() => Enum.GetValues<TriggerType>();
        [HttpGet("towerDirectionType")]
        public TowerDirectionType[] GetTowerDirectionType() => Enum.GetValues<TowerDirectionType>();
        [HttpGet("statusPriorityType")]
        public StatusPriorityType[] GetStatusPriorityType() => Enum.GetValues<StatusPriorityType>();
        [HttpGet("direction8Type")]
        public Direction8Type[] GetDirection8Type() => Enum.GetValues<Direction8Type>();
        [HttpGet("direction9Type")]
        public Direction9Type[] GetDirection9Type() => Enum.GetValues<Direction9Type>();
        [HttpGet("effectType")]
        public EffT[] GetEffectType() => Enum.GetValues<EffT>();


    }
}
