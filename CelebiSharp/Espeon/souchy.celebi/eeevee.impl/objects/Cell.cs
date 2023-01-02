using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util.math;

namespace Espeon.souchy.celebi.espeon.eevee.impl.objects
{
    public class Cell : ICell
    {
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }
        public IID modelUid { get; set; }

        public IPosition position { get; init; } = new Position();
        public List<IID> statuses { get; init; } = new List<IID>();
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public Dictionary<ContextType, IContext> contextsStats { get; set; } = new Dictionary<ContextType, IContext>();

        public Cell(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Espeon.GetUIdGenerator(fightUid).next();
            Espeon.GetRequiredScoped<IBoard>(fightUid).cells.Add(this);
        }

        public void Dispose()
        {
            Espeon.DisposeIID(fightUid, entityUid);
        }
    }
}
