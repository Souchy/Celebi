using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.objects.effects.special
{
    /// <summary>
    /// Get a value from the targets of this effect.
    /// This value is then stored in the IContext to make it available for other effects to use
    /// </summary>
    public interface IEffectGetValue : IEffect
    {
        /// <summary>
        /// Example: "entity.stats.Movement.current", "entity.stats.Movement.current>3"
        /// thats how we need to do conditions??????
        /// </summary>
        public string PropertyPath { get; set; }
        /// <summary>
        /// Example: "targetCurrentMP"
        /// </summary>
        public string AssignContextName { get; set; }

        /// <summary>
        /// If the value is a number, we have the possibility to return the sum
        /// </summary>
        public bool NumberSumTargets { get; set; }
        /// <summary>
        /// If the value is a number, we have the possibility to return the average
        /// </summary>
        public bool NumberAverageTargets { get; set; }
        /// <summary>
        /// If the value is a bool, 
        /// </summary>
        public bool BoolAnyTarget { get; set; }
    }
}
