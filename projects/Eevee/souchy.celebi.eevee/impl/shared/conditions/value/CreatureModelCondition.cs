using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class CreatureModelCondition : Condition, ICreatureModelCondition
    {
        public string creatureModelId { get; set; }

        public override bool check(IID fightId, IID source, IID target)
        {
            if(!this.checkChildren(fightId, source, target)) 
                return false;

            var fight = Eevee.fights.Get(fightId);
            if(creatureModelId == "S") { // take the source as the reference id
                var model1 = fight.creatures.Get(target).modelUid;
                var model2 = fight.creatures.Get(source).modelUid;
                return this.comparator.check(model1, model2);
            } else {
                IID checkable = this.actorType == ActorType.Source ? source : target;
                var crea = fight.creatures.Get(checkable);
                var model = crea.modelUid;
                return this.comparator.check(model, creatureModelId);
            }
        }
    }
}