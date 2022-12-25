namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketCastSpell
    {
        public int casterId { get; init; }
        public int targetCellId { get; init; }
        public int spellModelId { get; init; }
        public object[] compiledEffects { get; init; }
    }
}
