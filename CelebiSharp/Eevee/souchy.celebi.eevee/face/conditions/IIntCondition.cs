using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.conditions
{
    public interface IIntCondition : ICondition
    {
        public int value { get; set; }
        public ConditionComparatorType comparator { get; set; }
    }
}
