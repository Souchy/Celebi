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
        //public ConditionType conditionType { get; init; }
        public ActorType actorType { get; set; }
        /// <summary>
        /// Idk anymore if we need this for every condition, but we need a bool to check if we want this condition to be true or false (ex: "visible" vs "!visible")
        /// </summary>
        public ConditionComparatorType comparator { get; set; }

        public bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget);

        public ICondition copy();
    }

}
