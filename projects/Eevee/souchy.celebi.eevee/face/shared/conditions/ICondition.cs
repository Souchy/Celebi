using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.conditions
{

    public enum ConditionGroupType
    {
        AND,
        OR
    }

    public interface ICondition
    {
        public MomentType targetContext { get; set; }

        public ConditionType conditionType { get; set; }
        public ActorType actorType { get; set; }
        public ConditionComparatorType comparator { get; set; }

        public ConditionGroupType groupType { get; set; }
        public IEntityList<ICondition> children { get; set; }
        
        public bool check(IID fightId, IID source, IID target);
    }
}
