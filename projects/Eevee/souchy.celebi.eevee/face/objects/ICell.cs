using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects
{
    public interface ICell : IBoardEntity
    {
        public CellType type { get; set; }
        public IMapAsset assetOverride { get; set; }
        //public bool isWalkable { get; set; }
        //public bool blocksLos { get; set; }

    }
}