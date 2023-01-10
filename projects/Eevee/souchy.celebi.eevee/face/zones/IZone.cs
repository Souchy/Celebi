using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.util.math;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.zones
{
    public interface IZone
    {
        /// <summary>
        /// World origin: source or target of the spelleffect  <br></br>
        /// aka the zone Anchor in the world
        /// </summary>
        public ActorType worldOrigin { get; set; }
        /// <summary>
        /// offset from cast cell to local origin in the direction of the orientation
        /// </summary>
        public Vector2 worldOffset { get; set; }
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
        public Direction9Type rotation { get; set; }
        /// <summary>
        /// This overrides a size variable like length/radius to make them equal to .distance(target,source)
        /// </summary>
        public bool extendFromSource { get; set; }
        /// <summary>
        /// Get the cells touched by this area at target point
        /// </summary>
        public IArea getArea(IPosition targetCell);
    }

    public interface ZoneMulti : IZone
    {
        public List<IZone> zones { get; set; }
    }

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
        public IValue<int> angle { set; get; }
    }
    public interface ZoneSquare : IZone
    {
        public IValue<int> radiusMin { set; get; }
        public IValue<int> radiusMax { set; get; }
        public IValue<int> angle { set; get; }
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

}