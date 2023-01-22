using Espeon.souchy.celebi.espeon;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.eeevee.impl.controllers
{
    public class Player : IPlayer
    {
        public event OnChanged Changed;
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }

        public ITeam team { get; set; }
        public List<IID> creatures { get; set; } = new List<IID>();

        public Player(ScopeID scopeId)
        {
            fightUid = scopeId;
            entityUid = Scopes.GetUIdGenerator(fightUid).next();
            Scopes.GetRequiredScoped<IFight>(scopeId).players.Add(this);
        }

        public void Dispose()
        {
            Scopes.DisposeIID(fightUid, entityUid);
            creatures.Clear();
        }
    }
}