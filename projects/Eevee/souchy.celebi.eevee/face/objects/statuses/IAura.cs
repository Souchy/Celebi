using souchy.celebi.eevee.face.shared.zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.statuses
{
    public interface IAura : IStatusContainer
    {
        //public IZone zone { get; set; }


        // idk if we need zone + filter because the effects already have that 
        //      we may just set the parameters on the effects
    }
}
