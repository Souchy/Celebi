using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.face.shared.conditions
{

    public enum ConditionGroupType
    {
        AND,
        OR
    }

    public interface ICondition
    {
        //public ConditionType conditionType { get; }
        public ActorType actorType { get; set; }
        public ConditionComparatorType comparator { get; set; }
        public ConditionGroupType groupType { get; set; }
        public IEntityList<ICondition> children { get; set; }
        
        public bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget);
    }
}
