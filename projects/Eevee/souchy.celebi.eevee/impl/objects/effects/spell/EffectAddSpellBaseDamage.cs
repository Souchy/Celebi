using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects.effects.spell
{
    public class EffectAddSpellBaseDamage : Effect, IEffectAddSpellBaseDamage
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();
        public IValue<IID> EffectId { get; set; } = new Value<IID>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectAddSpellBaseDamage() { }
        private EffectAddSpellBaseDamage(IID id) : base(id) { }
        public static IEffectAddSpellBaseDamage Create() => new EffectAddSpellBaseDamage(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
