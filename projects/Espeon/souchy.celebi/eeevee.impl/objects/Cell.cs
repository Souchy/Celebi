using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.objects
{
    public class Cell : ICell
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID fightUid { get; init; }
        public IID modelUid { get; set; }

        public IPosition position { get; init; } = new Position();
        public List<IID> statuses { get; init; } = new List<IID>();
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public Dictionary<ContextType, IContext> contexts { get; set; } = new Dictionary<ContextType, IContext>();

        public Cell(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            //this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            //Scopes.GetRequiredScoped<IFight>(fightUid).cells.Add(entityUid, this);
            //Scopes.GetRequiredScoped<IBoard>(fightUid).cells.Add(entityUid);
            this.GetFight().cells.Add(entityUid, this);
            this.GetFight().board.cells.Add(entityUid);
        }

        public void Dispose()
        {
            this.GetFight().statuses.Remove(s => statuses.Contains(s.entityUid));
            Eevee.DisposeIID(this);
            //Scopes.DisposeIID(fightUid, entityUid);
        }
    }
}