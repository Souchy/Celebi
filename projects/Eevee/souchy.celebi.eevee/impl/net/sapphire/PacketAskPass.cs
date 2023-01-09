namespace souchy.celebi.eevee.impl.net.sapphire
{
    public interface PacketAskPass
    {
        // the server should already know who sent the packet
        // then you just put a restriction to not pass turn in the first 2 seconds of a turn
        // to avoid accidents and concurrency/latency

        //public int roundId { get; set; }
        //public int turnId { get; set; }
        //public int playerId { get; set; }
        //public int creatureId { get; set; }
    }
}
