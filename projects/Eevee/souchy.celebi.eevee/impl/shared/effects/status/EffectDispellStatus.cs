using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.shared.effects.creature;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public class EffectDispellStatus : Effect, IEffectDispellStatus
    {
        public IStatusCondition Filter { get; set; } //= new StatusCondition();
        //public IValue<bool> DispellCompletely { get; set; }
        public IValue<int> DispellTurns { get; set; } = new Value<int>();


        private EffectDispellStatus() { }
        private EffectDispellStatus(IID id) : base(id) { }
        public static IEffectDispellStatus Create() => new EffectDispellStatus(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
