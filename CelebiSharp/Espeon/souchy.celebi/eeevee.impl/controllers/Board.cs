using Microsoft.Extensions.DependencyInjection;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util;

namespace Espeon.souchy.celebi.espeon.eevee.impl.controllers
{
    public class Board : IBoard
    {
        #region Properties
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }

        public List<ICreature> creatures { get; init; } = new List<ICreature>();
        public List<ICell> cells { get; init; } = new List<ICell>();
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public Board(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Espeon.GetUIdGenerator(fightUid).next();
        }
        #endregion

        #region Public Methods
        public ICell getCell(IPosition pos)
        {
            return cells.First(c => c.position == pos);
        }

        public ICreature getCreature(IPosition pos)
        {
            return creatures.First(c => c.position == pos);
        }

        public List<ICreature> getCreatures(IPosition pos)
        {
            var list = new List<ICreature>();
            foreach (var c in creatures)
                if (c.position.x == pos.x && c.position.y == pos.y)
                    list.Add(c);
            return list;
        }

        public bool hasCreature(IPosition pos)
        {
           return creatures.Any(c => c.position == pos);
        }

        public void Dispose()
        {
            Espeon.DisposeIID(fightUid, entityUid);
            this.creatures.ForEach(c => c.Dispose());
            this.cells.ForEach(c => c.Dispose());
        }
        #endregion

    }
}
