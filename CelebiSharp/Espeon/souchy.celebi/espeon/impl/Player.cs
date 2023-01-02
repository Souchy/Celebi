using Espeon.souchy.celebi.espeon.eevee.impl.controllers;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using System.Numerics;

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
            this.entityUid = Espeon.GetUIdGenerator(fightUid).next();
            Espeon.GetRequiredScoped<IFight>(scopeId).players.Add(this);
        }

        public void Dispose()
        {
            Espeon.DisposeIID(fightUid, entityUid);
            creatures.Clear();
        }
    }
}
