using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.face.shared
{
    public interface IEffectsContainer
    {
        public IEntityList<ObjectId> EffectIds { get; set; }
        public IEnumerable<IEffect> GetEffects(); // => effectIds.Values.Select(i => Eevee.models.effects.Get(i));

        public IEnumerable<IEffect> checkTriggers(IAction action, TriggerEvent triggerEvent) // TriggerOrderType orderType, EffectPreview e);
        {
            return GetEffects()
                .Where(effect =>
                    effect.Triggers.Values.Any(t => 
                        t.checkTrigger(action, triggerEvent)
                    )
                );
        }

    }
}
