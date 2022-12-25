namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketMoveTo
    {
        public int casterId { get; init; }
        public int targetCellId { get; init; }
        public int[] path { get; set; }
    }
}
