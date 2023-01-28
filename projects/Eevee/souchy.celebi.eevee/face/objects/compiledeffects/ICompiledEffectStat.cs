using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.compiledeffects
{
    public interface ICompiledEffectStat : ICompiledEffect
    {

        public StatType statType { get; set; }
        public IStat stat { get; set; }

    }
}
