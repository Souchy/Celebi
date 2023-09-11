using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.objects
{
    public class Player : IPlayer
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public ObjectId fightUid { get; set; }
        public ITeam team { get; set; }
        public IEntityList<ObjectId> creatures { get; set; } = new EntitySet<ObjectId>();

        private Player() { }
        private Player(ObjectId fightId, ObjectId id)
        {
            fightUid = fightId;
            entityUid = id;
            this.GetFight().players.Add(this.entityUid, this);
        }
        public static IPlayer Create(ObjectId fightId) => new Player(fightId, Eevee.RegisterIIDTemporary());

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }
    }
}
