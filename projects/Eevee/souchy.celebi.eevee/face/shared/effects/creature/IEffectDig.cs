using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.creature
{
    public interface IEffectDig : IEffect
    {
        public IValue<int> depth { get; set; }
    }
}
