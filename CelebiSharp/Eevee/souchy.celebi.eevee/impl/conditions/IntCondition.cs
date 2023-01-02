using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.conditions
{
    public class IntCondition : IIntCondition
    {
        public ConditionGroupType groupType { get; set; }
        public List<ICondition> children { get; set; }


        public int value { get; set; }
        public ConditionComparatorType comparator { get; set; }
        public MomentType targetContext { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
