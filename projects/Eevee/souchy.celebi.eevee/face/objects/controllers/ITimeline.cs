using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.controllers
{
    public interface ITimeline : IFightEntity
    {

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
