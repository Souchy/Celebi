using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class DistanceCondition : Condition, IDistanceCondition
    {
        public int distance { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if(!this.checkChildren(action, trigger, boardSource, boardTarget)) //fightId, source, target))
                return false;

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