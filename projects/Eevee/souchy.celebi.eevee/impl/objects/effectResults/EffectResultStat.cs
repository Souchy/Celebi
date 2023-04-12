using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.enums.characteristics;

namespace souchy.celebi.eevee.impl.objects.effectResults
{
    public class EffectResultStat : EffectResult, IEffectResultStat
    {
        public CharacteristicId statId { get; set; }
        public IStat stat { get; set; }

        public EffectResultStat() { }
        public EffectResultStat(CharacteristicId statType, IStat stat)
        {
            this.statId = statType;
            this.stat = stat;
        }

        public override void apply0(IFight fight)
        {
            throw new NotImplementedException();
        }
    }
}
