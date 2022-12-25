namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketFight
    {
        public int[] players { get; set; }
        public int[] creatures { get; set; }
        public int mapId { get; set; }

    }
}
