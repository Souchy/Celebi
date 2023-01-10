namespace souchy.celebi.eevee.enums
{
    public enum IZoneType
    {
        point,
        multi, // multi zones in one

        line,

        circle,
        cone,
        square,
        rectangle,

        diagonal, // diag = line rotated 45°
        cross,
        xcross,
        star
    }
    public sealed class ZoneType
    {
        // star = square + 2 lines + 2 diagonals
        public readonly ZoneType point = new ZoneType("P");
        public readonly ZoneType multi = new ZoneType("M");
        public readonly ZoneType line = new ZoneType("L");
        public readonly ZoneType circle = new ZoneType("C");
        public readonly ZoneType cone = new ZoneType("V");
        public readonly ZoneType square = new ZoneType("S");
        public readonly ZoneType rectangle = new ZoneType("R");

        public readonly string name;
        private ZoneType(string name) => this.name = name;

    }

}