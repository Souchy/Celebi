using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface ISpellCondition : IStatCondition
    {
        public int spellModelId { get; set; }
    }
}
