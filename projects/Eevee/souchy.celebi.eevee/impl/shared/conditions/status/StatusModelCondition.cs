using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.conditions.status
{
    public class StatusModelCondition : Condition
    {
        public StatusIID statusModelId { get; set; }
        public BoardTargetType boardTargetType { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if (!checkChildren(action, trigger, boardSource, boardTarget))
                return false;
            IBoardEntity targ = actorType == ActorType.Source ? boardSource : boardTarget;
            targ = boardTargetType switch
            {
                BoardTargetType.Cell => targ,
                BoardTargetType.Creature => action.fight.GetBoardEntity(targ.entityUid),
                _ => throw new Exception()
            };
            IStatusContainer status = targ.GetStatuses().FirstOrDefault(s => s.modelUid == statusModelId);

            return status != null;
        }
    }
}
