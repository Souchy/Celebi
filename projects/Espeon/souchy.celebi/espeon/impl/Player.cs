using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.espeon.impl
{
    public class Player : IPlayer
    {
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }

        public ITeam team { get; set; }
        public List<IID> creatures { get; set; } = new List<IID>();

        public Player(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            Scopes.GetRequiredScoped<IFight>(scopeId).players.Add(this);
        }

        public void Dispose()
        {
            Scopes.DisposeIID(fightUid, entityUid);
            creatures.Clear();
        }
    }
}