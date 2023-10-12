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
        public ObjectId entityUid { get; set; }

        /// <summary>
        /// Trigger Data
        /// </summary>
        public ITriggerSchema schema { get; set; }
        public TriggerOrderType triggerOrderType { get; set; } = TriggerOrderType.After;
        public IZone triggerZone { get; set; }  // only targets in the zone can trigger the TriggerModel
        public ICondition triggererFilter { get; set; } // who can trigger the triggerModel
        public ICondition HolderCondition { get; set; } // if the holder can be triggered

        private TriggerModel() { }
        public static ITriggerModel Create() => new TriggerModel()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public ITriggerModel copy()
        {
            var copy = TriggerModel.Create();
            copy.triggerOrderType = triggerOrderType;
            copy.triggerZone = triggerZone.copy();
            copy.triggererFilter = triggererFilter.copy();
            copy.HolderCondition = HolderCondition.copy();
            copy.schema = schema.copy();
            return copy;
        }

        public bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            var caster = action.fight.creatures.Get(action.caster);
            var targetCell = action.fight.GetBoardEntity(action.targetCell);

            if (HolderCondition != null && !HolderCondition.check(action, triggerEvent, caster, targetCell))
                return false;
            if (triggererFilter != null && !triggererFilter.check(action, triggerEvent, caster, targetCell))
                return false;

            if (triggerZone != null)
            {
                var area = triggerZone.getArea(action.fight, targetCell.position);
                var isCasterInArea = area.Cells.Any(c => c.position == caster.position);
                if (!isCasterInArea)
                    return false;
            }

            var isRightType = this.schema.triggerType == triggerEvent.type && this.triggerOrderType == triggerEvent.orderType;
            if (!isRightType)
                return false;


            return this.schema.checkTrigger(action, triggerEvent);
        }

        public void Dispose()
        {
        }

    }

    public record TriggerEvent(
        TriggerType type, // what you react to (spellcast, passturn, ...)
        TriggerOrderType orderType // when you react to it (before, after)
        //IAction action //IEntityModeled entity = null // could be Effect, Spell, ... (OnEffectX, OnCastX...)
    );


    public interface ITriggerSchema
    {
        public TriggerType triggerType { get; init; }
        public bool checkTrigger(IAction action, TriggerEvent triggerEvent);
        public ITriggerSchema copy();
    }

    public abstract class TriggerSchema : ITriggerSchema
    {
        public TriggerType triggerType { get; init; }
        public TriggerSchema()
        {
            this.triggerType = TriggerType.getByType(GetType());
        }
        public abstract bool checkTrigger(IAction action, TriggerEvent triggerEvent);

        public ITriggerSchema copy()
        {
            throw new NotImplementedException();
        }
    }

}
