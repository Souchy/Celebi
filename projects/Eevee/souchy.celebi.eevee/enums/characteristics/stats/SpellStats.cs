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
    /// Spells could also have growth and evolutions by themselves
    /// ex: pick fireball, it turns into fireburst at lvl 6 (turn 6)
    /// </summary>
    public class SpellStats : Stats
    {
        private SpellStats() { }
        public static new SpellStats Create() => new SpellStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        protected override IStats copyImplementation(bool anonymous = false)
        {
            if (anonymous) return new SpellStats();
            else return SpellStats.Create();
        }

        // charges remaining
        // cd remaining
        // # casts per entity this turn
    }

}
