using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.spell
{
    public class EffectAddSpellBaseDamage : Effect, IEffectAddSpellBaseDamage
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();
        public IValue<IID> EffectId { get; set; } = new Value<IID>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectAddSpellBaseDamage() { }
        private EffectAddSpellBaseDamage(IID id) : base(id) { }
        public static IEffectAddSpellBaseDamage Create() => new EffectAddSpellBaseDamage(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
