using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.eevee.neweffects
{
    public class EffectPermanent : Effect, IEffectPermanent
    {
        public override IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => Eevee.models.effects.Get(i));
    }
}
