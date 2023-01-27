using Espeon.souchy.celebi.espeon;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.eeevee.impl.controllers
{
    public class Player : IPlayer
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID fightUid { get; init; }

        public ITeam team { get; set; }
        public List<IID> creatures { get; set; } = new List<IID>();

        public Player(ScopeID scopeId)
        {
            fightUid = scopeId;
            this.GetFight().players.Add(entityUid, this);
        }

        public void Dispose()
        {
            creatures.Clear();
            // dispose originaly owned creatures of this player
            this.GetFight().creatures.Remove(c => c.originalOwnerUid.value == this.entityUid);
            // reset owner of currently owned creatures of this player
            this.GetFight().creatures.Values.Where(c => c.currentOwnerUid.value == this.entityUid).ToList()
                .ForEach(c => c.currentOwnerUid = c.originalOwnerUid);

            Eevee.DisposeIID(this);
        }
    }
}