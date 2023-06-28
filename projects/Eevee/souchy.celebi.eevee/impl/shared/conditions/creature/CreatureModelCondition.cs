using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.shared.conditions.creature
{
    public class CreatureModelCondition : Condition //, ICreatureModelCondition
    {
        public bool sameAsSource { get; set; } = false;
        public CreatureIID creatureModelId { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if (!checkChildren(action, trigger, boardSource, boardTarget))
                return false;

            var fight = action.fight; // Eevee.fights.Get(fightId);
            if (sameAsSource)
            { //creatureModelId == "sameAsSource") { // take the source as the "reference" id to compare with
                var targetCrea = fight.board.GetCreatureOnCell(action.targetCell);
                var model1 = targetCrea.modelUid;
                var model2 = fight.creatures.Get(action.caster).modelUid;
                return comparator.check(model1, model2);
            }
            else
            {
                if (actorType == ActorType.Source)
                {
                    ObjectId checkable = action.caster; // this.actorType == ActorType.Source ? action.caster : action.targetCell;
                    var sourceModel = fight.creatures.Get(checkable).modelUid;
                    return comparator.check(sourceModel, creatureModelId);
                }
                else
                {
                    var targetModel = fight.board.GetCreatureOnCell(action.targetCell).modelUid;
                    return comparator.check(targetModel, creatureModelId);
                }
            }
        }
    }
}