using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.neweffects.impl.effects;
using souchy.celebi.eevee.neweffects.impl.scripts.creature;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.neweffects.impl.util
{
    public static class DamageUtil
    {
        //ElementType element, int baseDamage,
        public static int calculateDamage(IStats sourceStats, IStats targetStats, AbstractDamageSchema props, bool applyOffensiveStats = true, bool applyDefensiveStats = true, int additionalMultiplier = 100)
        {
            var element = props.element;
            var baseDamage = props.baseDamage;
            var pen = props.percentPenetration;
            var variance = props.percentVariance; // TODO

            // apply affinities + resistances
            int affEle = sourceStats.Get<IStatSimple>(element.GetAffinity()).value;
            int affDmg = sourceStats.Get<IStatSimple>(Affinity.Damage).value;
            int affPen = sourceStats.Get<IStatSimple>(Affinity.PenetrationPercent).value;
            //var dm = sourceStats.GetValue<IStatSimple, int>(Affinity.Damage, 0);

            // TODO calculate distance affinities for damage
            //var affDist = targetStats.Get<IStatSimple>(Affinity.Distance)?.value;
            //var affMelee = targetStats.Get<IStatSimple>(Affinity.Melee)?.value;
            //var dist = caster.position.sub(target.position).distance();
            int affdist = 0; // dist > 1 ? affDist : affMelee

            int res = targetStats.Get<IStatSimple>(element.GetResistance()).value;
            int resg = targetStats.Get<IStatSimple>(Resistance.Damage).value;

            // TODO damage calculation distance resistance
            //int resdist = targetStats.Get<IStatSimple>(Resistance.Distance).value + targetStats.Get<IStatSimple>(Resistance.Melee).value;
            int resdist = 0;

            double damage = baseDamage;
            double dmgMulti = 1;
            double resMulti = 1;
            if (applyOffensiveStats)
            {
                dmgMulti *= (100d + affEle) / 100d;
                dmgMulti *= (100d + affDmg) / 100d;
                dmgMulti *= (100d + affdist) / 100d;
            }
            if (applyDefensiveStats)
            {
                resMulti *= (100d - res) / 100d;
                resMulti *= (100d - resg) / 100d;
                resMulti *= (100d - resdist) / 100d;
                resMulti *= (100d - (affPen + pen)) / 100d; // ignores x% of resistance multiplier
            }
            damage *= dmgMulti;
            damage *= resMulti;
            damage *= additionalMultiplier / 100d;

            return (int) Math.Floor(damage);
        }


        public static int calculateHeal(IStats sourceStats, IStats targetStats, int baseHeal, ElementType element = ElementType.None, bool applyOffensiveStats = true, bool applyDefensiveStats = true, int additionalMultiplier = 100)
        {
            // apply affinities + resistances
            IStatSimple affEle = sourceStats.Get<IStatSimple>(element.GetAffinity());
            IStatSimple affHeal = sourceStats.Get<IStatSimple>(Affinity.Heal);
            int affdist = targetStats.Get<IStatSimple>(Affinity.Distance).value + targetStats.Get<IStatSimple>(Affinity.Melee).value;

            IStatSimple resHeal = targetStats.Get<IStatSimple>(Resistance.Heal);

            double heal = baseHeal;
            double healMulti = 1;
            double resMulti = 1;
            if (applyOffensiveStats)
            {
                healMulti *= (100d + affEle.value) / 100d;
                healMulti *= (100d + affHeal.value) / 100d;
                healMulti *= (100d + affdist) / 100d;
            }
            if (applyDefensiveStats)
            {
                resMulti *= (100d - resHeal.value) / 100d;
            }
            heal *= healMulti;
            heal *= resMulti;
            heal *= additionalMultiplier / 100d;

            return (int) Math.Floor(heal);
        }

    }
}
