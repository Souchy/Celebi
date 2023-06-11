using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.conditions.creature;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.stats;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class StatsCondition : Condition, IStatsCondition
    {
        public IStats conditionStats { get; set; } = Stats.Create();

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if(!this.checkChildren(action, trigger, boardSource, boardTarget)) 
                return false;

            ObjectId checkable = this.actorType == ActorType.Source ? action.caster : action.targetCell;
            var creature = action.fight.creatures.Get(checkable);
            var creatureStats = creature.GetTotalStats(action); //.@base;

            foreach (var conditionPair in conditionStats.@base.Pairs)
            {
                if (!creatureStats.Has(conditionPair.Key)) return false;
                var stat = creatureStats.Get(conditionPair.Key);
                if (!this.comparator.check(stat.genericValue, conditionPair.Value.genericValue))
                    return false;
            }

            // I doubt we could compare equations, they could be completely different curves with ups & downs
            //foreach (var pair in stats.growth.Pairs)
            //{
            //    var eq = creature.GetTotalStats(action).growth.Get(pair.Key);
            //    if (!this.comparator.check(eq., pair.Value.genericValue))
            //        return false;
            //}

            return true;
        }

    }
}