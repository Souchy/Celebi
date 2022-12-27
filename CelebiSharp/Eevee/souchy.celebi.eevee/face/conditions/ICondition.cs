namespace souchy.celebi.eevee.face.conditions
{

    public enum ConditionGroupType
    {
        AND,
        OR
    }

    public interface ICondition
    {

        public ConditionGroupType groupType { get; set; }
        public List<ICondition> children { get; set; }

    }
}
