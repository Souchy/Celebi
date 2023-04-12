using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.special;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.special
{
    /// <summary>
    /// Cast the children effects from the target location
    /// </summary>
    public class EffectRebase : Effect, IEffectRebase
    {

        private EffectRebase() { }
        private EffectRebase(IID id) => entityUid = id;
        public static IEffectRebase Create() => new EffectRebase(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
