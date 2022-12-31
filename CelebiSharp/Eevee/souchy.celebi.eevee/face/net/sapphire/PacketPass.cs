using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketPass
    {
        public int roundId { get; set; }
        public int turnId { get; set; }
        public IID playerId { get; set; }
        public IID creatureId { get; set; }
    }
}
