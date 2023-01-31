using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects.special;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectOvertimeDamage : Effect, IEffectOvertimeDamage
    {

        private EffectOvertimeDamage() { }
        private EffectOvertimeDamage(IID id) : base(id) { }
        public static IEffectOvertimeDamage Create() => new EffectOvertimeDamage(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
