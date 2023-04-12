using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface IStatCondition : ICondition //: IIntCondition
    {
        public CharacteristicId statId { get; set; }
        public object value { get; set; } // could be int or double or bool
    }
}
