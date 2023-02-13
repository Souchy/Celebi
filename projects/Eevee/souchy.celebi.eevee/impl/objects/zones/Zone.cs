using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
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
        public IValue<ZoneType> zoneType { get; set; } = new Value<ZoneType>(ZoneType.point);
        public IValue<IVector3> size { get; set; } = new Value<IVector3>(new Vector3());
        public bool negative { get; set; } = false;
        public ActorType worldOrigin { get; set; } = ActorType.Source;
        public IVector2 worldOffset { get; set; } = new Vector2();
        public Direction9Type localOrigin { get; set; } = Direction9Type.center;
        public Direction9Type rotation { get; set; } = Direction9Type.center;
        public IValue<bool> canRotate { get; set; } = new Value<bool>(false);
        public int sizeIndexExtendFromSource { get; set; } = -1;
        public IEntityList<IZone> children { get; set; } = new EntityList<IZone>(); //EntityList<IZone>.Create();


        // this isn't an entity and doesn't hold complex objects so it's good as public
        public Zone() { }
        //private Zone() { }
        //public static IZone Create() => new Zone();


        public IBoardArea getArea(IPosition targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
