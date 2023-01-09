namespace souchy.celebi.eevee.impl.net.umbreon
{
    public interface PacketJoinFight
    {
        public int[] players { get; set; }
        public int[] creatures { get; set; }
        public int mapId { get; set; }

    }
}
