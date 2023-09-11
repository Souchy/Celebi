using souchy.celebi.espeon;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using MongoDB.Bson;
using static souchy.celebi.eevee.face.entity.IEntity;
using souchy.celebi.eevee;
using MongoDB.Bson.Serialization.Attributes;

namespace souchy.celebi.espeon.eevee.impl.controllers
{
    public class Player : IPlayer
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public ObjectId fightUid { get; set; }

        public ITeam team { get; set; }
        public IEntityList<ObjectId> creatures { get; set; } = new EntityList<ObjectId>();

        public Player(ScopeID scopeId)
        {
            fightUid = scopeId;
            entityUid = Eevee.RegisterIIDTemporary();
            this.GetFight().players.Add(entityUid, this);
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            // reset owner of currently owned creatures of this player
            this.GetFight().creatures.Values.Where(c => c.currentOwnerUid == entityUid).ToList()
                .ForEach(c => c.currentOwnerUid = c.originalOwnerUid);
            // dispose originaly owned creatures of this player
            this.GetFight().creatures.Remove(c => c.originalOwnerUid == entityUid); // TODO creature.dispose
            creatures.Clear();

        }
    }
}
