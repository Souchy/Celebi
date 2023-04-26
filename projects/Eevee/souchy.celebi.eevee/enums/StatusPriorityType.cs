using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums
{
    public enum StatusPriorityType
    {
        /// <summary>
        /// System statuses include:
        ///     - death OnDamageTaken
        ///     - 
        /// </summary>
        System,
        /// <summary>
        /// Regular creature passives, cast at the start of the game
        /// </summary>
        Passive,
        /// <summary>
        /// Regular statuses. Maybe we want multiple levels of priority
        /// </summary>
        Status
    }
}
