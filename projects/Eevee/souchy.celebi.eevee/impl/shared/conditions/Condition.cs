using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.shared.conditions
{
    public abstract class Condition : ICondition
    {
        public ActorType actorType { get; set; } = ActorType.Target;
        public ConditionComparatorType comparator { get; set; } = ConditionComparatorType.EQ;
        public ConditionGroupType groupType { get; set; } = ConditionGroupType.AND;
        public IEntityList<ICondition> children { get; set; } = new EntityList<ICondition>();

        public abstract bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget);

        public bool checkChildren(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if(children.Values.Count == 0) 
                return true;
            if (groupType == ConditionGroupType.AND)
            {
                bool result = true;
                foreach (var c in children.Values)
                {
                    result &= c.check(action, trigger, boardSource, boardTarget);
                }
                return result;
            }
            if (groupType == ConditionGroupType.OR)
            {
                bool result = false;
                foreach (var c in children.Values)
                {
                    result |= c.check(action, trigger, boardSource, boardTarget);
                }
                return result;
            }
            return true;
        }

    }
}
