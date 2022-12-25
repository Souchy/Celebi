using souchy.celebi.eevee.face.filter;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects
{
    public interface IEffectDispellStatus : IEffect
    {
        public IStatusFilter filter { get; set; }

        public IValue<bool> dispellCompletely { get; set; }
        public IValue<int> dispellTurns { get; set; }

    }
}
