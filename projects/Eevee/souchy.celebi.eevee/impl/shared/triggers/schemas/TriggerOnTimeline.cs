using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{

    public record TriggerEventTimeline(TriggerOrderType orderType, MomentType moment) : TriggerEvent(TriggerType.TriggerOnTimeline, orderType)
    {
    }

    public class TriggerOnTimeline : TriggerSchema
    {
        // fight start
        // fight end
        // round start
        // round end
        // turn start
        // turn end
        public MomentType moment { get; set; } = MomentType.TurnStart;

        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            TriggerEventTimeline timelineEvent = (TriggerEventTimeline) triggerEvent;

            return timelineEvent.moment.Equals(moment);
        }
    }

}
