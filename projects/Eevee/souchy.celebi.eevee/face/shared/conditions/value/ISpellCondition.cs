using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface ISpellCondition : IStatCondition //: IIntCondition
    {
        public int spellModelId { get; set; }
    }
}
