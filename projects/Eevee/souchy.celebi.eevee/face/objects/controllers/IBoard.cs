using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;

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
    }
}