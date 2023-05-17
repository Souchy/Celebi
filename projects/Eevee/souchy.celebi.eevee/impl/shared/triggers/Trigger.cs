using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;

namespace souchy.celebi.eevee.impl.shared.triggers
{
    public class Trigger : ITrigger
    {
        public TriggerType TriggerType { get; set; }
        public TriggerOrderType TriggerOrderType { get; set; } = TriggerOrderType.After;
        public IZone TriggerZone { get; set; }
        public ICondition TriggererFilter { get; set; }
        public ICondition HolderCondition { get; set; }
    }

    public record TriggerEvent(
        TriggerType type,
        TriggerOrderType orderType,
        IEntityModeled entity = null // could be Effect, Spell, ... (OnEffectX, OnCastX...)
    );

}
