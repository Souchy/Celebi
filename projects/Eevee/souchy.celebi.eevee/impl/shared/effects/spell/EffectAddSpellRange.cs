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
    public class EffectAddSpellRange : Effect, IEffectAddSpellRange
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectAddSpellRange() { }
        private EffectAddSpellRange(IID id) : base(id) { }
        public static IEffectAddSpellRange Create() => new EffectAddSpellRange(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
