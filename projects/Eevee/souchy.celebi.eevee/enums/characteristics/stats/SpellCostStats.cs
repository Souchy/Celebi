using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.other
{
    /// <summary>
    /// Resources
    /// </summary>
    public class SpellCostStats : Stats
    {
        private SpellCostStats() { }
        public static new SpellCostStats Create() => new SpellCostStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
    }

}
