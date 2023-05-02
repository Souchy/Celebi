using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.shared.triggers;

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

        public bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            var caster = action.fight.creatures.Get(action.caster); //action.fight.GetBoardEntity(action.caster);
            var targetCell = action.fight.GetBoardEntity(action.targetCell); //.cells.Get(action.targetCell);

            if (!holderCondition.check(action, triggerEvent, caster, targetCell))
                return false;
            if (!triggererFilter.check(action, triggerEvent, caster, targetCell))
                return false;

            var area = zone.getArea(action.fight, targetCell.position);
            var isCasterInArea = area.Cells.Any(c => c.position == caster.position);
            if (!isCasterInArea) 
                return false;

            var isRightType = this.type == triggerEvent.type && this.orderType == triggerEvent.orderType;
            if (!isRightType) 
                return false;

            return true;
        }
    }
}
