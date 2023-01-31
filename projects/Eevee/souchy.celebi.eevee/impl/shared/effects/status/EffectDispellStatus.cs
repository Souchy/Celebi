using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects.creature;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public class EffectDispellStatus : Effect, IEffectDispellStatus
    {
        public IStatusCondition filter { get; set; }
        public IValue<bool> dispellCompletely { get; set; }
        public IValue<int> dispellTurns { get; set; }


        private EffectDispellStatus() { }
        private EffectDispellStatus(IID id) : base(id) { }
        public static IEffectDispellStatus Create() => new EffectDispellStatus(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
