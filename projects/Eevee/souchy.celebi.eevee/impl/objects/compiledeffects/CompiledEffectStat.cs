using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.compiledeffects
{
    public class CompiledEffectStat : CompiledEffect, ICompiledEffectStat
    {
        public StatType statType { get; set; }
        public IStat stat { get; set; }

        public CompiledEffectStat() { }
        public CompiledEffectStat(StatType statType, IStat stat)
        {
            this.statType = statType;
            this.stat = stat;
        }

        public override void apply(IFight fight)
        {
            throw new NotImplementedException();
        }
    }
}
