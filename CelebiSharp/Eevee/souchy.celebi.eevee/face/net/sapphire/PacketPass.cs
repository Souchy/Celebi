namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketPass
    {
        public int roundId { get; set; }
        public int turnId { get; set; }
        public int playerId { get; set; }
        public int creatureId { get; set; }
    }
}
