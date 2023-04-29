using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "[controller]")]
    public class EnumsController
    {
        [HttpGet("charac/characteristicCategory")]
        public CharacteristicCategory[] GetCharacteristicCategories() => Enum.GetValues<CharacteristicCategory>();

        [HttpGet("charac/resource")]
        public List<Resource> GetResource() => Resource.values.Values.ToList();
        [HttpGet("charac/resourceType")]
        public ResourceProperty[] GetResourceType() => Enum.GetValues<ResourceProperty>();
        [HttpGet("charac/resourceProperty")]
        public ResourceEnum[] GetResourceProperty() => Enum.GetValues<ResourceEnum>();

        [HttpGet("charac/affinty")]
        public List<Affinity> GetAffinity() => Affinity.values.Values.ToList();
        [HttpGet("charac/resistance")]
        public List<Resistance> GetResistance() => Resistance.values.Values.ToList();
        [HttpGet("charac/contextual")]
        public List<Contextual> GetContextual() => Contextual.values.Values.ToList();
        [HttpGet("charac/other")]
        public List<OtherProperty> GetOther() => OtherProperty.values.Values.ToList();
        [HttpGet("charac/spellmodel")]
        public List<SpellModelProperty> GetSpellModelProperty() => SpellModelProperty.values.Values.ToList();
        [HttpGet("charac/spell")]
        public List<SpellProperty> GetSpellProperty() => SpellProperty.values.Values.ToList();
        [HttpGet("charac/state")]
        public List<State> GetState() => State.values.Values.ToList();
        [HttpGet("charac/statusmodel")]
        public List<StatusModelProperty> GetStatusModelProperty() => StatusModelProperty.values.Values.ToList();

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
        

    }
}
