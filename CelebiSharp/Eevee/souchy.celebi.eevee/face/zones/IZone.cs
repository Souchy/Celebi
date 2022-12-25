using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.util.math;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.zones
{
    public interface IZone
    {
        /// <summary>
        /// offset from cast cell to local origin in the direction of the orientation
        /// </summary>
        public Vector2 localOffset { get; set; }
        /// <summary>
        /// center for a circle/square
        /// center for line perpendicular
        /// bottom for line
        /// top for line from source
        /// </summary>
        public Direction9Type localOrigin { get; set; }
        /// <summary>
        /// Rotation of the zone around the localOrigin
        /// </summary>
        public Direction9Type orientation { get; set; }
        /// <summary>
        /// This overrides a size variable like length/radius to make them equal to 
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