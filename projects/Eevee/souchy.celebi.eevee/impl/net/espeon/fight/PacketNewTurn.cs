namespace souchy.celebi.eevee.impl.net.espeon
{
    public interface PacketNewTurn
    {
        public int roundId { get; set; }
        public int turnId { get; set; }
        public int creatureId { get; set; }
    }
}
