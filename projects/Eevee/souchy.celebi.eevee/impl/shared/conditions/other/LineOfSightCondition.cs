using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.shared.conditions.other
{
    public class LineOfSightCondition : Condition
    {
        public bool lineOfSight { get; set; } = false;

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            throw new NotImplementedException();
        }

        public override ICondition copyImplementation()
        {
            var copy = new LineOfSightCondition();
            copy.lineOfSight = lineOfSight;
            return copy;
        }

    }
}
