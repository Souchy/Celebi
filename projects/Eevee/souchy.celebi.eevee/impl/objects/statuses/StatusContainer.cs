using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.objects;
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
        public IID sourceEffectPermanent { get; set; }
        public ObjectId sourceCreature { get; set; }
        public ObjectId holderEntity { get; set; }
        public ObjectId statsId { get; set; }

        public List<IStatusInstance> instances { get; set; } = new List<IStatusInstance>();

        protected StatusContainer(ObjectId id, ObjectId fightUid)
        {
            this.entityUid = id;
            this.fightUid = fightUid;
            //this.statsId = Eevee.RegisterIIDTemporary(); // found this way later, not sure about it
        }
        public static IStatusContainer Create(ObjectId fightId)
        {
            var status = new StatusContainer(fightId, Eevee.RegisterIIDTemporary());
            status.GetFight().statuses.Add(status.entityUid, status);
            return status;
        }


        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            ((IStatusContainer) this).GetStats().Dispose();
        }
    }
}
