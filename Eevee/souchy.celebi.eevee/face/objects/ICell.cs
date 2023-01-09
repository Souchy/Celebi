using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.objects
{
    public interface ICell : IBoardEntity
    {
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }


    }
}