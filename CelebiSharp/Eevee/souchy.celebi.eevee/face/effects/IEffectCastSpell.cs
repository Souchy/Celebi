using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects
{
    public interface IEffectCastSpell : IEffect
    {

        public IValue<int> spellModelId { get; set; }

    }
}
