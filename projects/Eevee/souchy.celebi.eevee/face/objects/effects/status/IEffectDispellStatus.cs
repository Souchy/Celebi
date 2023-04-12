using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.effects.status
{
    public interface IEffectDispellStatus : IEffect
    {
        public IStatusCondition Filter { get; set; }
        //public IValue<bool> DispellCompletely { get; set; }
        /// <summary>
        /// 0 for completely/infinite dispell
        /// </summary>
        public IValue<int> DispellTurns { get; set; }

    }
}
