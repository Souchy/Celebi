using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface IStatCondition : ICondition //: IIntCondition
    {
        public StatType statId { get; set; }
        public object value { get; set; } // could be int or double or bool
    }
}
