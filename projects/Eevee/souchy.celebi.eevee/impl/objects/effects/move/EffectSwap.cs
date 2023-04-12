using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// Swap position with the target if there's a creature there
    /// </summary>
    public class EffectSwap : Effect, IEffectSwap
    {

        private EffectSwap() { }
        private EffectSwap(IID id) : base(id) { }
        public static IEffectSwap Create() => new EffectSwap(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
