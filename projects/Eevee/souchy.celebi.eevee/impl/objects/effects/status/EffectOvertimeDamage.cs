using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.status;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.status
{
    public class EffectOvertimeDamage : Effect, IEffectOvertimeDamage
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectOvertimeDamage() { }
        private EffectOvertimeDamage(IID id) : base(id) { }
        public static IEffectOvertimeDamage Create() => new EffectOvertimeDamage(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
