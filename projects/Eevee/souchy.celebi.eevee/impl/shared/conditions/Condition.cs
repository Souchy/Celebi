using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.shared.conditions
{
    public abstract class Condition : ICondition
    {
        public MomentType targetContext { get; set; }

        public ConditionType conditionType { get; set; }
        public ActorType actorType { get; set; }
        public ConditionComparatorType comparator { get; set; }

        public ConditionGroupType groupType { get; set; }
        public IEntityList<ICondition> children { get; set; } = new EntityList<ICondition>();

        public abstract bool check(IID fightId, IID source, IID target);

        public bool checkChildren(IID fightId, IID source, IID target)
        {
            if(children.Values.Count == 0) 
                return true;
            if (groupType == ConditionGroupType.AND)
            {
                bool result = true;
                foreach (var c in children.Values)
                {
                    result &= c.check(fightId, source, target);
                }
                return result;
            }
            if (groupType == ConditionGroupType.OR)
            {
                bool result = false;
                foreach (var c in children.Values)
                {
                    result |= c.check(fightId, source, target);
                }
                return result;
            }
            return true;
        }

    }
}