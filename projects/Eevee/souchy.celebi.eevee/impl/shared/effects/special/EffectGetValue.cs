using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects.res;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.special
{
    /// <summary>
    /// Get a value from the targets of this effect.
    /// This value is then stored in the IContext to make it available for other effects to use
    /// </summary>
    public class EffectGetValue : Effect, IEffectGetValue
    {
        /// <summary>
        /// Example: "entity.stats.Movement.current", "entity.stats.Movement.current>3"
        /// thats how we need to do conditions??????
        /// </summary>
        public string PropertyPath { get; set; } = "";
        /// <summary>
        /// Example: "targetCurrentMP"
        /// </summary>
        public string AssignContextName { get; set; } = "";

        /// <summary>
        /// If the value is a number, we have the possibility to return the sum of all targets in aoe
        /// </summary>
        public bool NumberSumTargets { get; set; } = false;
        /// <summary>
        /// If the value is a number, we have the possibility to return the average of all targets in aoe 
        /// </summary>
        public bool NumberAverageTargets { get; set; } = false;
        /// <summary>
        /// If the value is a bool, check if any of the targets in aoe have the property to true
        /// </summary>
        public bool BoolAnyTarget { get; set; } = false;


        private EffectGetValue() { }
        private EffectGetValue(IID id) : base(id) { }
        public static IEffectGetValue Create() => new EffectGetValue(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
