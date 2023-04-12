using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.res;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.res
{
    public class EffectIndirectDamage : Effect, IEffectIndirectDamage
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectIndirectDamage() { }
        private EffectIndirectDamage(IID id) => entityUid = id;
        public static IEffectIndirectDamage Create() => new EffectIndirectDamage(Eevee.RegisterIID<IEffect>());
        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
