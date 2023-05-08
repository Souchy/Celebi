using Espeon.souchy.celebi.espeon;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.espeon.eeevee.impl.controllers
{
    public class Player : IPlayer
    {
        public IID entityUid { get; set; }
        public IID fightUid { get; set; }

        public ITeam team { get; set; }
        public IEntityList<IID> creatures { get; set; } = new EntityList<IID>();

        public Player(ScopeID scopeId)
        {
            fightUid = scopeId;
            entityUid = Eevee.RegisterIID<IPlayer>();
            this.GetFight().players.Add(entityUid, this);
        }

        public void Dispose()
        {
            creatures.Clear();
            // dispose originaly owned creatures of this player
            this.GetFight().creatures.Remove(c => c.originalOwnerUid == entityUid);
            // reset owner of currently owned creatures of this player
            this.GetFight().creatures.Values.Where(c => c.currentOwnerUid == entityUid).ToList()
                .ForEach(c => c.currentOwnerUid = c.originalOwnerUid);

            Eevee.DisposeIID<IPlayer>(entityUid);
        }
    }
}
