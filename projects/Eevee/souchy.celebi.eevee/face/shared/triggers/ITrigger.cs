using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;

namespace souchy.celebi.eevee.face.shared.triggers
{
    public interface ITrigger
    {
        /// <summary>
        /// Filter what kind of creature can proc this trigger (observation subject), ex: breed is a demon
        /// </summary>
        public ICondition triggererFilter { get; set; }

        /// <summary>
        /// Conditions on the holder of the trigger buff, ex: hp higher than 50
        /// </summary>
        public ICondition holderCondition { get; set; }

        /// <summary>
        /// Conditions for the event, ex: moved (walked, teleported), damageReceived, turnStart, etc.
        /// </summary>
        //public ICondition triggerConditions { get; set; }
        public TriggerType type { get; set; }

    }
}
