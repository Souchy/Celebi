using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.conditions
{
    public class IntCondition : IIntCondition
    {
        public ConditionGroupType groupType { get; set; }
        public IEntityList<ICondition> children { get; set; } = new EntityList<ICondition>(); // EntityList<ICondition>.Create();


        public int value { get; set; }
        public ConditionComparatorType comparator { get; set; }
        public MomentType targetContext { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
