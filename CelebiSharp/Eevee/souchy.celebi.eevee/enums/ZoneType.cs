namespace souchy.celebi.eevee.enums
{
    public enum IZoneType
    {
        point,
        multi,
        line,
        square,
        rectangle,
        circle,
        cone
    }
    public sealed class ZoneType
    {
        // star = square + 2 lines + 2 diagonals
        public readonly ZoneType point = new ZoneType("P");
        public readonly ZoneType multi = new ZoneType("M");
        public readonly ZoneType line = new ZoneType("L");
        public readonly ZoneType square = new ZoneType("S");
        public readonly ZoneType rectangle = new ZoneType("R");
        public readonly ZoneType circle = new ZoneType("C");
        public readonly ZoneType cone = new ZoneType("V");

        public readonly string name;
        private ZoneType(string name) => this.name = name;

    }

}