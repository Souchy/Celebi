﻿using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.shared.effects.special
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
        public string propertyPath { get; set; }
        /// <summary>
        /// Example: "targetCurrentMP"
        /// </summary>
        public string assignContextName { get; set; }

        /// <summary>
        /// If the value is a number, we have the possibility to return the sum
        /// </summary>
        public bool numberSumTargets { get; set; }
        /// <summary>
        /// If the value is a number, we have the possibility to return the average
        /// </summary>
        public bool numberAverageTargets { get; set; }
        /// <summary>
        /// If the value is a bool, 
        /// </summary>
        public bool boolAnyTarget { get; set; }
    }
}
