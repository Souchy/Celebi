using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.face.shared
{
    public interface IEffectsContainer
    {
        public IEntityList<IID> effectIds { get; set; }
        public IEnumerable<IEffect> GetEffects(); // => effectIds.Values.Select(i => Eevee.models.effects.Get(i));

        public IEnumerable<IEffect> checkTriggers(IAction action, TriggerEvent triggerEvent) // TriggerOrderType orderType, EffectPreview e);
        {
            return GetEffects()
                .Where(effect =>
                    effect.triggers.Values.Any(t => 
                        t.checkTrigger(action, triggerEvent)
                    )
                );
        }

    }
}
