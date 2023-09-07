using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.impl.shared.conditions.status
{
    public class StatusStatsCondition : Condition //, IStatusCondition
    {
        public int statusModelId { get; set; }
        public BoardTargetType boardTargetType { get; set; }
        public IStats conditionStats { get; set; }
        //public CharacteristicId statId { get; set; }
        //public object value { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            //if (!checkChildren(action, trigger, boardSource, boardTarget))
            //    return false;

            IBoardEntity targ = actorType == ActorType.Source ? boardSource : boardTarget;
            targ = boardTargetType switch
            {
                BoardTargetType.Cell => targ,
                BoardTargetType.Creature => action.fight.GetBoardEntity(targ.entityUid),
                _ => throw new Exception()
            };

            IStatusContainer status = targ.GetStatuses().FirstOrDefault(s => s.modelUid == statusModelId);
            var statusStats = status.GetStats(); //TODO .GetTotalStats(action); //.@base;

            foreach (var conditionPair in conditionStats.@base.Pairs)
            {
                if (!statusStats.Has(conditionPair.Key)) return false;
                var stat = statusStats.Get(conditionPair.Key);
                if (!comparator.check(stat.genericValue, conditionPair.Value.genericValue))
                    return false;
            }
            return true;


            //var stat = status.GetStats().Get(statId);
            //// var stat = creature.GetStats().Get((StatType) statId);

            //object fetchedValue = null;
            //if(stat is StatSimple statSimple) {
            //    fetchedValue = statSimple.value;
            //}
            //if(stat is StatBool statBool) {
            //    fetchedValue = statBool.value;
            //}
            //return this.comparator.check(fetchedValue, value);
        }
    }
}