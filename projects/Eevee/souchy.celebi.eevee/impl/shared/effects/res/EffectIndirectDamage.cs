using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectIndirectDamage : Effect, IEffectIndirectDamage
    {

        public EffectIndirectDamage() { }
        private EffectIndirectDamage(IID id) => entityUid = id;
        public static IEffectIndirectDamage Create() => new EffectIndirectDamage(Eevee.RegisterIID());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
