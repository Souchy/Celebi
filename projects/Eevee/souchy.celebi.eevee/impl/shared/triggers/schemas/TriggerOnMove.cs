using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{
    public class TriggerOnMove : TriggerSchema
    {
        /// <summary>
        /// Walk, Teleport, Swap, Translate...
        /// </summary>
        public MoveType moveType { get; set; }

        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            return true;
        }
    }

    public class TriggerOnCellMovement : TriggerSchema
    {
        /// <summary>
        /// enter cell
        /// exit cell
        /// stop on cell
        /// </summary>
        public CellMovementType moveType { get; set; }

        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            return true;
        }
    }
}
