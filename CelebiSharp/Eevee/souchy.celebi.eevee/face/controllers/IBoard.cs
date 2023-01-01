using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.face.controllers
{
    public interface IBoard : IFightEntity
    {
        public List<ICreature> creatures { get; init; }
        public List<ICell> cells { get; init; }

        public ICell getCell(IPosition pos);

        public ICreature getCreature(IPosition pos);

        public List<ICreature> getCreatures(IPosition pos);

        public bool hasCreature(IPosition pos);
    }
}