using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util;

namespace Espeon.souchy.celebi.espeon.impl.eevee.controllers
{
    public class Board : IBoard
    {
        #region Properties
        public uint entityUid { get; init; }

        public List<ICreatureInstance> creatures { get; init; } = new List<ICreatureInstance>();
        public List<ICell> cells { get; init; } = new List<ICell>();
        #endregion

        #region Constants
        private readonly IUIdGenerator _uIdGenerator;
        #endregion

        #region Constructors
        public Board(IUIdGenerator uIdGenerator)
        {
            _uIdGenerator = uIdGenerator;
            entityUid = uIdGenerator.next();
        }
        #endregion

        #region Public Methods
        public ICell getCell(IPosition pos)
        {
            return cells.First(c => c.position == pos);
        }

        public ICreatureInstance getCreature(IPosition pos)
        {
            return creatures.First(c => c.position == pos);
        }

        public List<ICreatureInstance> getCreatures(IPosition pos)
        {
            var list = new List<ICreatureInstance>();
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
            this._uIdGenerator.dispose(entityUid);
            this.creatures.ForEach(c => c.Dispose());
            this.cells.ForEach(c => c.Dispose());
        }
        #endregion

    }
}
