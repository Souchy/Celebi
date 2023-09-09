using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    /// <summary>
    /// We have multiple modes and even allow custom games.
    /// </summary>
    // Old comment in Timeline
    /// <summary>
    /// Game Mode A: like dofus, each creature plays one after the other <br></br>
    /// Game Mode B: like chess, you can play any creature on your team <para></para>
    /// 
    /// Timer Mode A: total game time <br></br>
    /// Timer Mode B: turn time <br></br>
    /// </summary>
    public class FightSettings
    {
        public FightSettings() { }
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public FightPreparationType preparationType { get; set; }
        /// <summary>
        /// Time per turn. Thinking of a blitz mode with like 10s per turn instead of 30s
        /// </summary>
        public int secondsPerTurn { get; set; } = 30;
        public int maximumGameTime { get; set; } = 0;
        public int maximumNumberOfTurns { get; set; } = 0;
        public int numberOfTeams { get; set; } = 2;
        public int numberOfCreaturesPerTeam { get; set; } = 5;
        public int numberOfcreaturesOnBoardPerTeam { get; set; } = 3;
    }

    public enum FightPreparationType
    {
        constructed = 1,
        draft = 2,
    }
}
