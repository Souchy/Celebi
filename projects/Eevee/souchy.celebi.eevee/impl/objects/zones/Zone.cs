using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.controllers;
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
    public enum TargetSamplingType
    {
        // careful, the origin can be placed on Actor.Source as well as Actor.Target
        closestToOrigin, 
        furthestToOrigin,
        closestToSource,
        furthestToSource,
        random,
    }

    public class Zone : IZone
    {
        public IValue<ZoneType> zoneType { get; set; } = new Value<ZoneType>(ZoneType.point);
        public IValue<IVector3> size { get; set; } = new Value<IVector3>(new Vector3(1, 0, 1));
        public bool negative { get; set; } = false;
        public ActorType worldOrigin { get; set; } = ActorType.Target;
        public IVector2 worldOffset { get; set; } = new Vector2();
        public Direction9Type localOrigin { get; set; } = Direction9Type.center;
        public Rotation4Type rotation { get; set; } = Rotation4Type.top;
        public IValue<bool> canRotate { get; set; } = new Value<bool>(false);
        public int sizeIndexExtendFromSource { get; set; } = -1;
        public IEntityList<IZone> children { get; set; } = new EntityList<IZone>(); //EntityList<IZone>.Create();
        public int maxSampleCount { get; set; } = int.MaxValue;
        public TargetSamplingType samplingType { get; set; } = TargetSamplingType.closestToOrigin;


        // this isn't an entity and doesn't hold complex objects so it's good as public
        public Zone() { }
        //private Zone() { }
        //public static IZone Create() => new Zone();


        public IArea getArea(IFight fight, IPosition targetCell)
        {
            throw new NotImplementedException();
        }

        public IZone copy()
        {
            var copy = new Zone();
            copy.zoneType.value = this.zoneType.value;
            copy.size.value = this.size.value.copy();
            copy.negative = negative;
            copy.worldOrigin = worldOrigin;
            copy.worldOffset = worldOffset;
            copy.localOrigin = localOrigin;
            copy.rotation = rotation;
            copy.canRotate.value = canRotate.value;
            copy.sizeIndexExtendFromSource = sizeIndexExtendFromSource;
            copy.maxSampleCount = maxSampleCount;
            copy.samplingType = samplingType;
            foreach(var child in children.Values)
                copy.children.Add(child.copy());
            return copy;
        }
    }
}
