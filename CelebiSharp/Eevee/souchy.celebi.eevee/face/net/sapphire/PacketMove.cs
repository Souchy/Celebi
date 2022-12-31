using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketMoveTo
    {
        public IID casterId { get; init; }
        public IID targetCellId { get; init; }
        public IID[] cellPathIds { get; set; }
    }
}
