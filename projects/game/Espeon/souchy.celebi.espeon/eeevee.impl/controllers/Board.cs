using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.espeon.eeevee.impl.controllers
{
    public class Board : IBoard
    {
        public IID entityUid { get; set; } = Eevee.RegisterIID<IBoard>();
        public IID fightUid { get; set; }

        public IEntityList<IID> creatureIds { get; init; } = new EntityList<IID>();
        public IEntityList<IID> cells { get; init; } = new EntityList<IID>();

        public Board(ScopeID scopeId)
        {
            fightUid = scopeId;
            this.GetFight().board = this;
            //this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            //this.instances = Scopes.GetRequiredScoped<IFight>(fightUid);
        }

        public void Dispose()
        {
            this.GetFight().board = null;
            Eevee.DisposeIID<IBoard>(entityUid);
            // nothing to dispose? 
            //throw new NotImplementedException();
        }

        /*
        #region Public Methods

        public ICell getCell(IPosition pos)
        {
            return cells.First(c => c.position == pos);
        }

        public ICreature getCreature(IPosition pos)
        {
            return Creatures.First(c => c.position == pos);
        }

        public List<ICreature> getCreatures(IPosition pos)
        {
            var list = new List<ICreature>();
            foreach (var c in Creatures)
                if (c.position.x == pos.x && c.position.y == pos.y)
                    list.Add(c);
            return list;
        }

        public bool hasCreature(IPosition pos)
        {
            return Creatures.Any(c => c.position == pos);
        }

        public void Dispose()
        {
            Scopes.DisposeIID(fightUid, entityUid);
            this.cells.ForEach(c => c.Dispose());
            this.cells.Clear();
            this.creatureIds.Clear();
        }

        #endregion Public Methods
        */
    }
}
