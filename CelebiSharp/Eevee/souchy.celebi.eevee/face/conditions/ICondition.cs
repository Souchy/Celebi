using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.conditions
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
