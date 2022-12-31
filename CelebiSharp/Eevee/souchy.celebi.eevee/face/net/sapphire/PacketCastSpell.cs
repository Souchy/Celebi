using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketCastSpell
    {
        public IID casterId { get; init; }
        public IID targetCellId { get; init; }
        public IID spellModelId { get; init; }
        public object[] compiledEffects { get; init; }
    }
}
