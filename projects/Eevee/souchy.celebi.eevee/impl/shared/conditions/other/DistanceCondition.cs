using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.conditions.other
{
    public class DistanceCondition : Condition
    {
        public int distance { get; set; } = 0;

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            //if (!checkChildren(action, trigger, boardSource, boardTarget)) //fightId, source, target))
            //    return false;

            var actualDistance = boardSource.position
                .distanceManhattan(boardTarget.position);
            //.copy()
            //.sub(boardTarget.position)
            //.set(IVector3.Y_INDEX, 0)
            //.abs()
            //.length();

            return this.comparator.check(actualDistance, distance);
        }
    }


}
