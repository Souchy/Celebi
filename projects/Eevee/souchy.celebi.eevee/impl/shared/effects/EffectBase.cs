using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects;
using souchy.celebi.eevee.face.shared.effects.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.effects
{
    public class EffectBase : Effect, IEffectBase
    {

        private EffectBase() { }
        private EffectBase(IID id) : base(id) { }
        public static IEffectBase Create() => new EffectBase(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            // just compile children
            throw new NotImplementedException();
        }
    }
}
