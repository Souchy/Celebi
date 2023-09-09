using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.controllers
{


    /// <summary>
    /// Game Mode A: like dofus, each creature plays one after the other <br></br>
    /// Game Mode B: like chess, you can play any creature on your team <para></para>
    /// 
    /// Timer Mode A: total game time <br></br>
    /// Timer Mode B: turn time <br></br>
    /// 
    /// </summary>
    public interface ITimeline : IFightEntity
    {
        /// <summary>
        /// Time per turn. Thinking of a blitz mode with like 10s per turn instead of 30s
        /// </summary>
        public int secondsPerTurn { get; init; }

        public int currentRound { get; set; }
        public int currentTurn { get; set; }

        /// <summary>
        /// As a baseline, the fastest (speed stat) creatures play first. <br></br>
        /// But the order may change when players swapOut/swapIn
        /// </summary>
        public List<ObjectId> creatureIds { get; init; }

        public IPlayer getCurrentPlayer();
        public ICreature getCurrentCreature();
        public IEnumerable<ICreature> getCreatures() => creatureIds.Select(crea => GetFight().creatures.Get(crea));

    }
}
