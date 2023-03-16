using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.zones;

namespace souchy.celebi.eevee.face.shared.triggers
{
    public interface ITrigger
    {
        /// <summary>
        /// Conditions for the event, ex: moved (walked, teleported), damageReceived, turnStart, etc.
        /// </summary>
        //public ICondition triggerConditions { get; set; }
        public TriggerType type { get; set; }

        /// <summary>
        /// Wethere to insert the triggered effects before or after the cause
        /// </summary>
        public TriggerOrderType orderType { get; set; }

        /// <summary>
        /// Trigger zone, only creatures in that zone can activate the trigger (may not need this if we use glyphs)
        /// </summary>
        public IZone zone { get; set; }

        /// <summary>
        /// Additionaly Filter what kind of creature can proc this trigger (observation subject), ex: breed is a demon
        /// </summary>
        public ICondition triggererFilter { get; set; }

        /// <summary>
        /// Additionaly Conditions on the holder of the trigger buff, ex: hp higher than 50
        /// </summary>
        public ICondition holderCondition { get; set; }
    }
}
