using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.eevee.impl.shared.triggers
{
    public class TriggerModel : ITriggerModel
    {
        //public TriggerType triggerType { get; set; }
        public TriggerOrderType triggerOrderType { get; set; } = TriggerOrderType.After;
        public IZone triggerZone { get; set; }  // only targets in the zone can trigger the TriggerModel
        public ICondition triggererFilter { get; set; } // who can trigger the triggerModel
        public ICondition HolderCondition { get; set; } // if the holder can be triggered
        /// <summary>
        /// Trigger Data
        /// </summary>
        public ITriggerSchema schema { get; set; }  
    }

    public record TriggerEvent(
        TriggerType type, // what you react to (spellcast, passturn, ...)
        TriggerOrderType orderType, // when you react to it (before, after)
        IAction action //IEntityModeled entity = null // could be Effect, Spell, ... (OnEffectX, OnCastX...)
    );


    public interface ITriggerSchema
    {
        public TriggerType triggerType
        {
            get
            {
                return TriggerType.getByType(this.GetType());
            }
        }
    }

}
