using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions.creature;

namespace souchy.celebi.eevee.face.shared.conditions.spell
{
    public interface ISpellCondition : IStatCondition
    {
        public int spellModelId { get; set; }
    }
}
