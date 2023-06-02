using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.zones;

namespace souchy.celebi.eevee.face.shared.zones
{
    public interface IZone
    {
        /// <summary>
        /// 
        /// </summary>
        public IValue<ZoneType> zoneType { get; set; }
        /// <summary>
        /// 1) x = lengthMin (forward length)
        /// 2) z = lengthMax (side length)
        /// 3) y = ring width: 0 = full shape, y = actual ring width
        /// </summary>
        public IValue<IVector3> size { get; set; }
        /// <summary>
        /// If we substract this zone from its parent and siblings
        /// </summary>
        public bool negative { get; set; }

        /// <summary>
        /// World origin: source or target of the spelleffect  <br></br>
        /// aka the zone Anchor in the world
        /// </summary>
        public ActorType worldOrigin { get; set; }
        /// <summary>
        /// offset from cast cell to local origin in the direction of the orientation
        /// </summary>
        public IVector2 worldOffset { get; set; }
        /// <summary>
        /// AKA the zone Anchor local to the aoe. <br></br>
        /// center for a circle/square <br></br>
        /// center for line perpendicular <br></br>
        /// bottom for line <br></br>
        /// top for line from source
        /// </summary>
        public Direction9Type localOrigin { get; set; }
        /// <summary>
        /// Rotation of the zone around the localOrigin
        /// </summary>
        public Rotation4Type rotation { get; set; }
        /// <summary>
        /// Wether the player can rotate the aoe manually or if it's fixed
        /// </summary>
        public IValue<bool> canRotate { get; set; }
        /// <summary>
        /// This overrides a size variable like length/radius to make them equal to .distance(target,source) <br></br>
        /// Example: iop fracture, sacri fulguration, ... ça créé une zone partant du lanceur jusqu'à la cible <br></br>
        /// The index given is the {1,2,3} value overriden in the vector.
        /// </summary>
        public int sizeIndexExtendFromSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEntityList<IZone> children { get; set; }
        /// <summary>
        /// If value = 4, it will take a maximum of 4 targets among the cells in the area <br></br>
        /// If value = 1, it will only take the first target in the area. (Good for bouncing skills) <br></br>
        /// If value = int.MaxValue, then it can take infinite targets obviously. <br></br>
        /// Targets are chosen/sampled in accordance to the samplingType.
        /// </summary>
        public int maxSampleCount { get; set; }
        /// <summary>
        /// If value = random, the targets in the area will be chosen at random <br></br>
        /// If value = closestToOrigin, the targets in the area will be chosen by order of closest to origin. <br></br>
        /// This is also the order in which effects are applied to the targets. <br></br>
        /// This respects maxSampleCount.
        /// </summary>
        public TargetSamplingType samplingType { get; set; }




        public int GetLengthForward() => size.value.x;
        public int GetLengthSide() => size.value.z;
        public int GetRingWidth() => size.value.y;
        /// <summary>
        /// Get the cells touched by this area at target point
        /// </summary>
        public IArea getArea(IFight fight, IPosition targetCell);

    }

    //public interface ZoneMulti : IZone
    //{
    //}

    /*
    public interface ZonePoint : IZone
    {

    }
    public interface ZoneLine : IZone
    {
        public IValue<int> lengthMin { set; get; }
        public IValue<int> lengthMax { set; get; }
    }
    public interface ZoneCircle : IZone
    {
        public IValue<int> radiusMin { set; get; }
        public IValue<int> radiusMax { set; get; }
        public IValue<int> completionAngle { set; get; }
    }
    public interface ZoneSquare : IZone
    {
        public IValue<int> radiusMin { set; get; }
        public IValue<int> radiusMax { set; get; }
        public IValue<int> completionAngle { set; get; }
    }
    public interface ZoneCone : IZone
    {
        public IValue<int> radiusMin { set; get; }
        public IValue<int> radiusMax { set; get; }
        //public IValue<int> angleTotal { set; get; }
    }
    public interface ZoneRectangle : IZone
    {
        public IValue<int> radiusMin { set; get; }
        public IValue<int> lengthMax { set; get; }
        public IValue<int> width { set; get; }
    }
    */

}