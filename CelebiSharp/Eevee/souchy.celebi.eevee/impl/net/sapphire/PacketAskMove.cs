namespace souchy.celebi.eevee.impl.net.sapphire
{
    public interface PacketAskMove
    {
        public int casterId { get; init; }
        public int targetCellId { get; init; }
    }
}
