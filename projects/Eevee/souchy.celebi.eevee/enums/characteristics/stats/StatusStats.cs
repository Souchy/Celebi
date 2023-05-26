using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.other
{
    public class StatusStats : Stats
    {
        private StatusStats() { }
        public static new StatusStats Create() => new StatusStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
        // #stacks
        //      delay
        //      duration

        // -- model ?
        // max stacks
        // max duration
        // max delay
    }

}
