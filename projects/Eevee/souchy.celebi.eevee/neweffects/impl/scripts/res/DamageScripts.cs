using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects.res;
using IEffect = souchy.celebi.eevee.neweffects.face.IEffect;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.neweffects.impl.effects
{
    public sealed class DirectDamageScript : ADamageScript
    {
        public override Type SchemaType => typeof(DirectDamage);
        protected override bool appliesOffensiveStats => true;
        protected override bool appliesDefensiveStats => true;
    }
    public sealed class DirectDamageStealLifeScript : ADamageScript
    {
        public override Type SchemaType => typeof(DirectDamageStealLife);
        protected override bool appliesOffensiveStats => true;
        protected override bool appliesDefensiveStats => true;
    }
    public sealed class DirectDamagePercentLifeMaxScript : ADamageScript
    {
        public override Type SchemaType => typeof(DirectDamagePercentLifeMax);
        protected override bool appliesOffensiveStats => false;
        protected override bool appliesDefensiveStats => true;
    }
    public sealed class IndirectDamageScript : ADamageScript
    {
        public override Type SchemaType => typeof(IndirectDamage);
        protected override bool appliesOffensiveStats => true;
        protected override bool appliesDefensiveStats => false;
    }
    public sealed class IndirectDamagePercentLifeMaxScript : ADamageScript
    {
        public override Type SchemaType => typeof(IndirectDamagePercentLifeMax);
        protected override bool appliesOffensiveStats => false;
        protected override bool appliesDefensiveStats => false;
    }
}
