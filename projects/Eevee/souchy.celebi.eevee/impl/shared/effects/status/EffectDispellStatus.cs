using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.effects;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public class EffectDispellStatus : Effect, IEffectDispellStatus
    {
        public IStatusCondition filter { get; set; }
        public IValue<bool> dispellCompletely { get; set; }
        public IValue<int> dispellTurns { get; set; }

    }
}
