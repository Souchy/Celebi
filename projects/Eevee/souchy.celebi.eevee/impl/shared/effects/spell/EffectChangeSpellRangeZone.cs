using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects.special;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.spell
{
    public class EffectChangeSpellRangeZone : Effect, IEffectChangeSpellRangeZone
    {

        public EffectChangeSpellRangeZone() { }
        private EffectChangeSpellRangeZone(IID id) : base(id) { }
        public static IEffectChangeSpellRangeZone Create() => new EffectChangeSpellRangeZone(Eevee.RegisterIID());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
