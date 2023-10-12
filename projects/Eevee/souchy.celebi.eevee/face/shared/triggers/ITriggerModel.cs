using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.face.shared.triggers
{
    public interface ITriggerModel : IEntity
    {
        /// <summary>
        /// Holds the TriggerType and trigger properties specific to each schema type
        /// </summary>
        public ITriggerSchema schema { get; set; }

        /// <summary>
        /// { When to react to something }
        /// Wether to insert the triggered effects before or after the cause
        /// </summary>
        public TriggerOrderType triggerOrderType { get; set; }

        /// <summary>
        /// { Where is the thing we can react to }
        /// Trigger zone, only creatures in that zone can activate the trigger (may not need this if we use glyphs)
        /// </summary>
        public IZone triggerZone { get; set; }

        /// <summary>
        /// { Who we can react to }
        /// Additionaly Filter what kind of creature can proc this trigger (observation subject), ex: breed is a demon
        /// </summary>
        public ICondition triggererFilter { get; set; }

        /// <summary>
        /// { How the holder is }
        /// Additionally Conditions on the holder of the trigger buff, ex: hp higher than 50
        /// </summary>
        public ICondition HolderCondition { get; set; }

        public bool checkTrigger(IAction action, TriggerEvent triggerEvent);

        public ITriggerModel copy();
    }
}
