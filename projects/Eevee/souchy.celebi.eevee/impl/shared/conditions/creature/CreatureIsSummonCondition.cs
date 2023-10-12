using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.shared.triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.conditions.creature
{
    public class CreatureIsSummonCondition : Condition
    {
        public bool isSummon { get; set; } = false;

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            throw new NotImplementedException();
        }

        public override ICondition copyImplementation()
        {
            var copy = new CreatureIsSummonCondition();
            copy.isSummon = isSummon;
            return copy;
        }
    }
}
