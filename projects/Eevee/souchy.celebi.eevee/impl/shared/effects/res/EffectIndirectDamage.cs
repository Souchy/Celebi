using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectIndirectDamage : Effect, IEffectIndirectDamage
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectIndirectDamage() { }
        private EffectIndirectDamage(IID id) => entityUid = id;
        public static IEffectIndirectDamage Create() => new EffectIndirectDamage(Eevee.RegisterIID<IEffect>());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
