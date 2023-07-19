using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.enums;
using System.Drawing;

namespace souchy.celebi.eevee.impl.objects
{
    public class Map : IMap
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public AssetIID? mapAsset { get; set; }
        public Dictionary<CellType, IMapAsset>? defaultAssets { get; set; } = new();
        public IVector3[][] teamsStartPositions { get; set; }
        public ICell[] cells { get; set; }

        private Map() { }
        public static IMap Create() => new Map()
        {
            entityUid = Eevee.RegisterIIDTemporary(),
        };

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }
    }

    public class MapAsset : IMapAsset
    {
        public AssetIID path { get; set; }
        public IVector3[] transform { get; set; }
        public Color color { get; set; }
    }

}
