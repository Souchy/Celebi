﻿using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.statuses
{
    public class StatusContainer : IStatusContainer
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public ObjectId fightUid { get; set; }
        public IID modelUid { get; set; }

        public IID sourceSpellModel { get; set; }
        public ObjectId sourceCreature { get; set; }
        public ObjectId holderEntity { get; set; }
        public ObjectId statsId { get; set; }

        public List<IStatusInstance> instances { get; set; } = new List<IStatusInstance>();

        protected StatusContainer(ObjectId id, ObjectId fightUid)
        {
            this.entityUid = id;
            this.fightUid = fightUid;
            this.statsId = Eevee.RegisterIIDTemporary();
        }
        public static IStatusContainer Create(ObjectId fightUid) => new StatusContainer(Eevee.RegisterIIDTemporary(), fightUid);


        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            ((IStatusContainer) this).GetStats().Dispose();
        }
    }
}
