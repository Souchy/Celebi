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
        public ConditionType conditionType { get; init; }
        public ActorType actorType { get; set; } = ActorType.Target;
        public ConditionComparatorType comparator { get; set; } = ConditionComparatorType.EQ;

        public Condition()
        {
            conditionType = ConditionType.getByType(GetType());
        }

        public abstract bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget);
        public ICondition copy()
        {
            var copy = copyImplementation();
            copy.actorType = actorType;
            copy.comparator = comparator;
            return copy;
        }
        public abstract ICondition copyImplementation();
    }

}
