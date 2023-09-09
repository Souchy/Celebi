using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    public class Timeline : ITimeline
    {
        public ObjectId entityUid { get; set; }
        public ObjectId fightUid { get; set; }

        public int currentRound { get; set; }
        public int currentTurn { get; set; }

        public List<ObjectId> creatureIds { get; init; } = new List<ObjectId>();

        public static ITimeline Create(ObjectId fightid)
        {
            var timeline = new Timeline()
            {
                fightUid = fightid,
                entityUid = Eevee.RegisterIIDTemporary()
            };
            timeline.GetFight().timeline = timeline;
            return timeline;
        }

        public ICreature getCurrentCreature()
        {
            throw new NotImplementedException();
        }

        public IPlayer getCurrentPlayer()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
