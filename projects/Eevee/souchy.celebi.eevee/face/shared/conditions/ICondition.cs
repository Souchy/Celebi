using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;

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
        public List<ICondition> children { get; set; }
    }
}
