using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.special;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.special
{
    /// <summary>
    /// Apply a random effect from its children
    /// </summary>
    public class EffectRandom : Effect, IEffectRandom
    {
        /// <summary>
        /// Weight chance to be picked for each child effect
        /// </summary>
        public List<int> Weights { get; set; } = new();

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }

    }
}
