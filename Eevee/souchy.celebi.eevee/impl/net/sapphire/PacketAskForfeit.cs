namespace souchy.celebi.eevee.impl.net.sapphire
{
    public interface PacketAskForfeit
    {
        /// <summary>
        /// Only need it from server to client
        /// </summary>
        public int playerId { get; set; }
    }
}
