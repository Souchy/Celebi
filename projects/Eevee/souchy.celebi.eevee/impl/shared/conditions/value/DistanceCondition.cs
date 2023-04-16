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
            if(!this.checkChildren(fightId, source, target))
                return false;

            var fight = Eevee.fights.Get(fightId);
            var e1 = fight.GetBoardEntity(source);
            var e2 = fight.GetBoardEntity(target);

            var actualDistance = e1.position.copy().sub(e1.position)
                .set(IVector3.Y_INDEX, 0).abs().length();
            
            return this.comparator.check(actualDistance, distance);
        }
    }
}