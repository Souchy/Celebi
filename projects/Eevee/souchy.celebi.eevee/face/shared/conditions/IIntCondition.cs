using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;

namespace souchy.celebi.eevee.face.conditions
{
    public interface IIntCondition : ICondition
    {
        public int value { get; set; }
        public ConditionComparatorType comparator { get; set; }
    }
}
