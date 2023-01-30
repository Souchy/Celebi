using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.controllers
{


    /// <summary>
    /// Game Mode A: like dofus, each creature plays one after the other
    /// Game Mode B: like chess, you can play any creature on your team
    /// 
    /// Timer Mode A: total game time
    /// Timer Mode B: turn time
    /// 
    /// </summary>
    public interface ITimeline
    {
        /// <summary>
        /// Time per turn. Thinking of a blitz mode with like 10s per turn instead of 30s
        /// </summary>
        public int secondsPerTurn { get; init; }

        public int currentRound { get; set; }
        public int currentTurn { get; set; }

        /// <summary>
        /// TODO?
        /// </summary>
        public IID GetCurrentPlayer();


    }
}
