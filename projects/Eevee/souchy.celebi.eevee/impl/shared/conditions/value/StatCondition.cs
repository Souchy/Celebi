using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class StatCondition : Condition, IStatCondition
    {
        public CharacteristicId statId { get; set; }
        public object value { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if(!this.checkChildren(action, trigger, boardSource, boardTarget)) 
                return false;
            ObjectId checkable = this.actorType == ActorType.Source ? action.caster : action.targetCell;
            var creature = action.fight.creatures.Get(checkable);
            var stat = creature.GetTotalStats(action) //, trigger)
                .Get(statId);

            object fetchedValue = null;
            if(stat is StatSimple statSimple) {
                fetchedValue = statSimple.value;
            }
            if(stat is StatBool statBool) {
                fetchedValue = statBool.value;
            }
            return this.comparator.check(fetchedValue, value);
        }

    }
}