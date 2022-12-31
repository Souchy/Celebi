using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects
{
    public interface IEffectDispellStatus : IEffect
    {
        public IStatusCondition filter { get; set; }
        public IValue<bool> dispellCompletely { get; set; }
        public IValue<int> dispellTurns { get; set; }

    }
}
