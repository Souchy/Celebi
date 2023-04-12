using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.status;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.status
{
    /// <summary>
    /// 
    /// </summary>
    public class EffectAddStatus : Effect, IEffectAddStatus
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();


        private EffectAddStatus() { }
        private EffectAddStatus(IID id) : base(id) { }
        public static IEffectAddStatus Create() => new EffectAddStatus(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
