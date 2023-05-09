using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.impl.objects
{
    public class Map : IMap
    {
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IID name { get; set; }
        public IVector3[][] teamsStartPositions { get; set; }
        public ICell[] cells { get; set; }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }
    }
}
