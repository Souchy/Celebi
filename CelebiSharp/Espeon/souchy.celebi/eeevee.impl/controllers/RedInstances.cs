using Espeon.souchy.celebi.espeon;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.eeevee.impl.controllers
{
    public class RedInstances : IRedInstances
    {
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }

        public Dictionary<IID, ICreature> creatures { get; init; } = new Dictionary<IID, ICreature>();
        public Dictionary<IID, ISpell> spells { get; init; } = new Dictionary<IID, ISpell>();
        public Dictionary<IID, IStatus> statuses { get; init; } = new Dictionary<IID, IStatus>();

        public RedInstances(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Espeon.souchy.celebi.espeon.Espeon.GetUIdGenerator(scopeId).next();
        }

        public void Dispose()
        {
            Espeon.souchy.celebi.espeon.Espeon.DisposeIID(fightUid, entityUid);
            foreach (var c in creatures.Values) c.Dispose();
            foreach (var c in spells.Values) c.Dispose();
            foreach (var c in statuses.Values) c.Dispose();
        }
    }
}
