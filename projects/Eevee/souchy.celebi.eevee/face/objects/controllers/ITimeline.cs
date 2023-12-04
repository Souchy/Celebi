using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
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
        public ObjectId currentTurn { get; set; }

        /// <summary>
        /// As a baseline, the fastest (speed stat) creatures play first. <br></br>
        /// But the order may change when players swapOut/swapIn
        /// </summary>
        //public List<ObjectId> creatureIds { get; init; }
        public List<TimelineSlot> slots { get; init; }

        public IPlayer getCurrentPlayer();
        public ICreature getCurrentCreature();
        public IEnumerable<ICreature> getCreatures() => slots.SelectMany(s => s.getAll()); //creatureIds.Select(crea => GetFight().creatures.Get(crea));


        public void nextRound(IAction action);
        public void nextTurn(IAction action);
        public int indexOf(ObjectId creatureId);
        public int size();
        public ICreature getCreatureAt(int i);
        public bool isCreatureOnBoard(ObjectId crea);
        public void addSlot(ObjectId crea);

    }
}