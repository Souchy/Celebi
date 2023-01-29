using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.special
{
    /// <summary>
    /// Apply a random effect from its children
    /// </summary>
    public class EffectRandom : Effect, IEffectRandom
    {
        /// <summary>
        /// Weight chance to be picked for each child effect
        /// </summary>
        public List<int> weights { get; set; }

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
