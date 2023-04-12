using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// 
    /// </summary>
    public class EffectTeleportSymmetrically : Effect, IEffectTeleportSymmetrically
    {
        public IValue<IPosition> Center { get; set; } = new Value<IPosition>();


        private EffectTeleportSymmetrically() { }
        private EffectTeleportSymmetrically(IID id) : base(id) { }
        public static IEffectTeleportSymmetrically Create() => new EffectTeleportSymmetrically(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
