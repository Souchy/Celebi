namespace souchy.celebi.eevee.impl.net.sapphire
{
    public interface PacketCastSpellResponse
    {
        public int casterId { get; init; }
        public int targetCellId { get; init; }
        public int spellModelId { get; init; }
        public object[] compiledEffects { get; init; }
    }
}
