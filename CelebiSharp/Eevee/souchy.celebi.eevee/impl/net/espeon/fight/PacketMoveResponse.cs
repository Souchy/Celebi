namespace souchy.celebi.eevee.impl.net.sapphire
{
    public interface PacketMoveResponse
    {
        public int casterId { get; init; }
        public int targetCellId { get; init; }
        public int[] path { get; set; }
    }
}
