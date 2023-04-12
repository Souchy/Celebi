using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.effectResults;

namespace souchy.celebi.eevee.face.shared
{
    public interface IEffectsContainer
    {
        public IEntityList<IID> effectIds { get; set; }
        public IEnumerable<IEffect> GetEffects(); // => effectIds.Values.Select(i => Eevee.models.effects.Get(i));
        
        public IEnumerable<EffectResult> checkTriggers(TriggerOrderType orderType, EffectResult e);
        
    }
}
