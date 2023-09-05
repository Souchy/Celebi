using souchy.celebi.eevee.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{

    public class TriggerOnTimeline : ITriggerSchema
    {
        public MomentType moment { get; set; }
        // fight start
        // fight end
        // round start
        // round end
        // turn start
        // turn end
    }

}
