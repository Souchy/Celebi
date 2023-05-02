namespace souchy.celebi.eevee.face.objects.effectResults
{
    /// <summary>
    /// Damage is just short for an effect that damages Life. 
    /// There can be different effects that hit mana instead.
    /// When compiled, it's just a value of life to remove
    /// </summary>
    public interface IEffectResultLifeDamage : IEffectPreview
    {
        /// <summary>
        /// Amount of life lost
        /// </summary>
        public int damage { get; set; }
    }
}
