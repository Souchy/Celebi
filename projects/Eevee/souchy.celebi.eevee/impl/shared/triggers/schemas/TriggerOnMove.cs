using souchy.celebi.eevee.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{
    internal class TriggerOnMove : ITriggerSchema
    {
        /// <summary>
        /// Walk, Teleport, Swap, Translate...
        /// </summary>
        public MoveType moveType { get; set; }
    }

    public class TriggerOnCellMovement : ITriggerSchema
    {
        /// <summary>
        /// enter cell
        /// exit cell
        /// stop on cell
        /// </summary>
        public CellMovementType moveType { get; set; }
    }
}
