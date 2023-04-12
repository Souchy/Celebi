using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;

namespace souchy.celebi.eevee.impl.shared.triggers
{
    public class Trigger : ITrigger
    {
        public TriggerType type { get; set; }
        public TriggerOrderType orderType { get; set; } = TriggerOrderType.After;
        public IZone zone { get; set; }
        public ICondition triggererFilter { get; set; }
        public ICondition holderCondition { get; set; }
    }

    public record TriggerEvent(TriggerType type, TriggerOrderType orderType = TriggerOrderType.After);

}
