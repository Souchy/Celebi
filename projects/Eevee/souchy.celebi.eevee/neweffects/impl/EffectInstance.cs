using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.eevee.neweffects
{
    public class EffectInstance : Effect, IEffectInstance
    {
        public ObjectId fightUid { get; set; }

        public override IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => this.GetFight().effects.Get(i));
    }
}
