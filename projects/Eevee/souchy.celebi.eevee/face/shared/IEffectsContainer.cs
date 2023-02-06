using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.shared
{
    public interface IEffectsContainer
    {
        public IEntityList<IID> effectIds { get; set; }
        public IEnumerable<IEffect> GetEffects(); // => effectIds.Values.Select(i => Eevee.models.effects.Get(i));
    }
}
