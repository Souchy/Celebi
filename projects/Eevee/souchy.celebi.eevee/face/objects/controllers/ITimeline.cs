using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.controllers
{
    public interface ITimeline
    {
        /// <summary>
        /// Time per turn. Thinking of a blitz mode with like 10s per turn instead of 30s
        /// </summary>
        public int secondsPerTurn { get; init; }

        public int currentRound { get; set; }
        public int currentTurn { get; set; }
    }
}
