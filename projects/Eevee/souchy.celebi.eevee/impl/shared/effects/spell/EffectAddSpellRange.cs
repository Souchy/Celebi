using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.spell
{
    public class EffectAddSpellRange : Effect, IEffectAddSpellRange
    {

        public EffectAddSpellRange() { }
        private EffectAddSpellRange(IID id) : base(id) { }
        public static IEffectAddSpellRange Create() => new EffectAddSpellRange(Eevee.RegisterIID());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
