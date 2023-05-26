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
    /// all status properties
    /// </summary>
    public class StatusModelStats : Stats
    {
        private StatusModelStats() { }
        public static new StatusModelStats Create() => new StatusModelStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
    }
    /// <summary>
    /// status container properties only
    /// </summary>
    public class StatusContainerStats : Stats
    {
        private StatusContainerStats() { }
        public static new StatusContainerStats Create() => new StatusContainerStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
    }
    /// <summary>
    /// status instance properties only
    /// </summary>
    public class StatusInstanceStats : Stats
    {
        private StatusInstanceStats() { }
        public static new StatusInstanceStats Create() => new StatusInstanceStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
    }

}
