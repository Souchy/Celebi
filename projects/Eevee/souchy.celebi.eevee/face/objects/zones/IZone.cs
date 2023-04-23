using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.zones
{
    public interface IZone
    {
        /// <summary>
        /// 
        /// </summary>
        public IValue<ZoneType> zoneType { get; set; }
        /// <summary>
        /// 1) lengthMin
        /// 2) lengthMax
        /// 3) Ring width: 0 = full shape, x = actual ring width
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
        /// This overrides a size variable like length/radius to make them equal to .distance(target,source)
        /// The index given is the {1,2,3} value overriden in the vector.
        /// </summary>
        public int sizeIndexExtendFromSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEntityList<IZone> children { get; set; }

        public int GetRingWidth() => size.value.z;


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