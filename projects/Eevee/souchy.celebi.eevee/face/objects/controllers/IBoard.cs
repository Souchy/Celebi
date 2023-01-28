using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.objects.controllers
{
    public interface IBoard : IFightEntity
    {
        /// <summary>
        /// List of creatures on the board <br></br>
        /// Creatures need to be ordered in timeline order
        /// </summary>
        public List<IID> creatureIds { get; init; }
        /// <summary>
        /// List of cells on the board
        /// </summary>
        public List<IID> cells { get; init; }


        // Board will be just a normal data object like the others.
        // Make something else for functions

        //public ICell getCell(IPosition pos);
        //public ICreature getCreature(IPosition pos);
        //public List<ICreature> getCreatures(IPosition pos);
        //public bool hasCreature(IPosition pos);

        public ICreature? getCreatureOnCell(IID targetCell)
        {
            var cell = this.GetFight().cells.Get(targetCell);
            ICreature? crea = this.GetFight().creatures.Values
                    .Where(c => c.position == cell.position)
                    .Where(c => creatureIds.Contains(c.entityUid))
                    .FirstOrDefault();
            return crea;
        }

    }
}