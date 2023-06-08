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
    /// Also includes resource costs
    /// </summary>
    public class SpellModelStats : Stats
    {
        private SpellModelStats() { }
        public static new SpellModelStats Create() => new SpellModelStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
        //----- Model
        // los
        // max charges
        // max cast per target
        // max cast per turn
        // cd
        // cd initial
        // cd global
        //// min range
        //// max range
        //// cast in diag
        //// cast in line
    }
}
