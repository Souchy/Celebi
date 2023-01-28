using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public interface IEffectDispellStatus : IEffect
    {
        public IStatusCondition filter { get; set; }
        public IValue<bool> dispellCompletely { get; set; }
        public IValue<int> dispellTurns { get; set; }

    }
}
