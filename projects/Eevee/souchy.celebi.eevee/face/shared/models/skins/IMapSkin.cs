using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.shared.models.skins
{
    public interface IMapSkin : IEntity
    {
        public ObjectId nameId { get; set; }

        public AssetIID mesh { get; set; }
        public AssetIID background { get; set; }

        public List<MapCellResource> resources { get; set; }
        public List<MapDecoration> decorations { get; set; }

        /// <summary>
        /// Default Resource ID for blocks
        /// </summary>
        public int defaultBlockResourceIndex { get; set; }
        public int defaultFloorResourceIndex { get; set; }
        public int defaultHoleResourceIndex { get; set; }

        public Dictionary<int, int> cellRessourceIndex { get; set; }
    }

    public class MapDecoration
    {
        public int resourceIndex { get; set; }
        public Vector3 position { get; set; }
    }



    public class MapCellResource
    {
        /// <summary>
        /// in the case of a pre-built model
        /// </summary>
        public AssetIID mesh { get; set; }
        /// <summary>
        /// { { transX, transY, transZ }, { rotX, rotY, rotZ }, { sclX, sclY, sclZ } }
        /// </summary>
        public float[,] transform { get; set; } = new float[3, 3];
        /// <summary>
        /// in the case of a voxel : 6 paths to 6 textures
        /// </summary>
        public AssetIID[] textures { get; set; }
        /// <summary>
        /// in the case of a voxel with 6 textures.
        /// coordinates are { x0, y0, x1, y1 } for each 6 textures
        /// </summary>
        public float[] coordinates { get; set; }
        /// <summary>
        /// contains 1 color if it's a model or 6 if it's a voxel
        /// </summary>
        public string[] colors { get; set; }
        /// <summary>
        /// if this data creates a voxel or a pre-built model
        /// </summary>
        public bool isVoxel() => textures != null && coordinates != null && textures.Length == 6 && coordinates.Length == 6;
    } 

}
