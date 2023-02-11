using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.zones
{
    public class Zone : IZone
    {
        public IValue<Vector3> size { get; set; } = new Value<Vector3>();
        public bool negative { get; set; }
        public ActorType worldOrigin { get; set; } = ActorType.Source;
        public Vector2 worldOffset { get; set; } //= new();
        public Direction9Type localOrigin { get; set; } = Direction9Type.center;
        public Direction9Type rotation { get; set; } = Direction9Type.center;
        public int sizeIndexExtendFromSource { get; set; }
        public IEntityList<IZone> children { get; set; } = new EntityList<IZone>(); //EntityList<IZone>.Create();


        // this isn't an entity and doesn't hold complex objects so it's good as public
        public Zone() { }
        //private Zone() { }
        //public static IZone Create() => new Zone();


        public IArea getArea(IPosition targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
