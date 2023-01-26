using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.effects.special
{
    /// <summary>
    /// Apply a random effect from its children
    /// </summary>
    public interface IEffectRandom : IEffect
    {
        /// <summary>
        /// Weight chance to be picked for each child effect
        /// </summary>
        public List<int> weights { get; set; }

    }
}
