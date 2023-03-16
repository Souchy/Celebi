using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface IStatCondition : ICondition //: IIntCondition
    {
        public int statId { get; set; }
        public Object value { get; set; } // could be int or double or bool
    }
}
