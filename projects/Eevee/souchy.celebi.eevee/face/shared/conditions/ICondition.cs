using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
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
        public ConditionGroupType groupType { get; set; }
        public IEntityList<ICondition> children { get; set; }
    }
}
