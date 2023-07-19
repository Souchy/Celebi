using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.skins
{
    public class MapSkin : IMapSkin
    {
        public ObjectId entityUid { get; set; }
        public ObjectId nameId { get; set; }
        public AssetIID mesh { get; set; }
        public AssetIID background { get; set; }
        public List<MapCellResource> resources { get; set; }
        public List<MapDecoration> decorations { get; set; }
        public int defaultBlockResourceIndex { get; set; }
        public int defaultFloorResourceIndex { get; set; }
        public int defaultHoleResourceIndex { get; set; }
        public Dictionary<int, int> cellRessourceIndex { get; set; }

        private MapSkin() { }
        public static IMapSkin Create() => new MapSkin()
        {
            entityUid = Eevee.RegisterIIDTemporary(),
        };


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
