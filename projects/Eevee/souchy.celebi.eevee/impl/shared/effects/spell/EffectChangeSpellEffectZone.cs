using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.spell
{
    public class EffectChangeSpellEffectZone : Effect, IEffectChangeSpellEffectZone
    {

        private EffectChangeSpellEffectZone() { }
        private EffectChangeSpellEffectZone(IID id) : base(id) { }
        public static IEffectChangeSpellEffectZone Create() => new EffectChangeSpellEffectZone(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
