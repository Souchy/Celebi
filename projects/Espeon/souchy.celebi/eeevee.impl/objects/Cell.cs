using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.objects
{
    public class Cell : ICell
    {
        public event OnChanged Changed;
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }
        public IID modelUid { get; set; }

        public IPosition position { get; init; } = new Position();
        public List<IID> statuses { get; init; } = new List<IID>();
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public Dictionary<ContextType, IContext> contexts { get; set; } = new Dictionary<ContextType, IContext>();

        public Cell(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            Scopes.GetRequiredScoped<IBoard>(fightUid).cells.Add(this);
        }

        public void Dispose()
        {
            Scopes.DisposeIID(fightUid, entityUid);
        }
    }
}