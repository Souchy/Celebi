using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.impl.shared.conditions.creature
{
    public class CreatureStatsCondition : Condition 
    {
        public bool useNaturalStats { get; set; } = false;
        public bool compareWithTarget { get; set; } = false;
        public IStats conditionStats { get; set; } = Stats.Create();

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if (!checkChildren(action, trigger, boardSource, boardTarget))
                return false;

            ObjectId checkable = actorType == ActorType.Source ? action.caster : action.targetCell;
            var creature = action.fight.creatures.Get(checkable);
            var creatureStats = useNaturalStats ? creature.GetNaturalStats() : creature.GetTotalStats(action); //.@base;

            if (!compareWithTarget)
            {
                foreach (var conditionPair in conditionStats.@base.Pairs)
                {
                    if (!creatureStats.Has(conditionPair.Key)) return false;
                    var actual = creatureStats.Get(conditionPair.Key).genericValue;
                    if (!comparator.check(actual, conditionPair.Value.genericValue))
                        return false;
                }
            }
            else
            {
                if (boardTarget is not ICreature) return false;
                var creaturetTarget = (ICreature)boardTarget;
                var targetStats = useNaturalStats ? creaturetTarget.GetNaturalStats() : creaturetTarget.GetTotalStats(action);
                foreach (var conditionPair in conditionStats.@base.Pairs)
                {
                    if (!creatureStats.Has(conditionPair.Key)) return false;
                    if (!targetStats.Has(conditionPair.Key)) return false;
                    var actual = delta(creatureStats.Get(conditionPair.Key), targetStats.Get(conditionPair.Key));
                    if (!comparator.check(actual, conditionPair.Value.genericValue))
                        return false;
                }
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


        /// <summary>
        /// for stats difference
        /// </summary>
        public object delta(object fetchedValue, object wantedValue)
        {
            if (fetchedValue == null) return null;
            if (wantedValue == null) return null;
            if (fetchedValue.GetType() == typeof(bool))
            {
                return wantedValue != fetchedValue;
            }
            else
            {
                double fetchedInt = (double)fetchedValue;
                double wantedInt = (double)wantedValue;
                return wantedInt - fetchedInt;
            }
        }
    }
}