using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions.creature;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class CreatureModelCondition : Condition, ICreatureModelCondition
    {
        public bool sameAsSource { get; set; } = false;
        public string creatureModelId { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if(!this.checkChildren(action, trigger, boardSource, boardTarget)) 
                return false;

            var fight = action.fight; // Eevee.fights.Get(fightId);
            if(sameAsSource){ //creatureModelId == "sameAsSource") { // take the source as the "reference" id to compare with
                var targetCrea = fight.board.GetCreatureOnCell(action.targetCell);
                var model1 = targetCrea.modelUid;
                var model2 = fight.creatures.Get(action.caster).modelUid;
                return this.comparator.check(model1, model2);
            } else {
                if(this.actorType == ActorType.Source)
                {
                    ObjectId checkable = action.caster; // this.actorType == ActorType.Source ? action.caster : action.targetCell;
                    var crea = fight.creatures.Get(checkable);
                    var model = crea.modelUid;
                    return this.comparator.check(model, creatureModelId);
                } else
                {
                    var targetCrea = fight.board.GetCreatureOnCell(action.targetCell);
                    var model = targetCrea.modelUid;
                    return this.comparator.check(model, creatureModelId);
                }
            }
        }
    }
}