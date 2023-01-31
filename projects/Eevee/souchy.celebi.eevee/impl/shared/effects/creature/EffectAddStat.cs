using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.effects.creature;
using souchy.celebi.eevee.face.shared.effects.status;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectAddStat : Effect, IEffectAddStat
    {

        public StatType statType { get; set; }
        public IStat stat { get; set; }


        private EffectAddStat() { }
        private EffectAddStat(IID id) : base(id) { }
        public static IEffectAddStat Create() => new EffectAddStat(Eevee.RegisterIID<IEffect>());


        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
