using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.objects;
using System.Drawing;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IMap : IEntityModel
    {
        public ObjectId nameId { get; set; }
        /// <summary>
        /// If it exists, it is shown.
        /// If you want to generate the map by celltype assets, make this null.
        /// </summary>
        public AssetIID? mapAsset { get; set; }
        /// <summary>
        /// If it exists, it is shown.
        /// If you want just the map asset from blender, make this null.
        /// </summary>
        public Dictionary<CellType, IMapAsset>? defaultAssets { get; set; }
        public IVector3[][] teamsStartPositions { get; set; }
        public ICell[] cells { get; set; }
    }

    public interface IMapAsset
    {
        public AssetIID path { get; set; }
        public IVector3[] transform { get; set; }
        public Color color { get; set; }
        // textures
    }
}
