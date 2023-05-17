using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.neweffects.impl.effects;

namespace souchy.celebi.eevee.neweffects.impl.util
{
    public static class DamageUtil
    {
        //ElementType element, int baseDamage,
        public static int calculateDamage(IStats sourceStats, IStats targetStats, ADamageSchema props, bool applyOffensiveStats = true, bool applyDefensiveStats = true, int additionalMultiplier = 100)
        {
            var element = props.element;
            var baseDamage = props.baseDamage;
            var pen = props.percentPenetration;

            // apply affinities + resistances
            IStatSimple affEle = sourceStats.Get<IStatSimple>(element.GetAffinity());
            IStatSimple affDmg = sourceStats.Get<IStatSimple>(Affinity.Damage);
            IStatSimple affPen = sourceStats.Get<IStatSimple>(Affinity.PenetrationPercent);

            int affdist = targetStats.Get<IStatSimple>(Affinity.Distance).value + targetStats.Get<IStatSimple>(Affinity.Melee).value;

            IStatSimple res = targetStats.Get<IStatSimple>(element.GetResistance());
            IStatSimple resg = targetStats.Get<IStatSimple>(Resistance.Damage);

            int resdist = targetStats.Get<IStatSimple>(Resistance.Distance).value + targetStats.Get<IStatSimple>(Resistance.Melee).value;

            double damage = baseDamage;
            double dmgMulti = 1;
            double resMulti = 1;
            if (applyOffensiveStats)
            {
                dmgMulti *= (100d + affEle.value) / 100d;
                dmgMulti *= (100d + affDmg.value) / 100d;
                dmgMulti *= (100d + affdist) / 100d;
            }
            if (applyDefensiveStats)
            {
                resMulti *= (100d - res.value) / 100d;
                resMulti *= (100d - resg.value) / 100d;
                resMulti *= (100d - resdist) / 100d;
                resMulti *= (100d - (affPen.value + pen)) / 100d; // ignores x% of resistance multiplier
            }
            damage *= dmgMulti;
            damage *= resMulti;
            damage *= additionalMultiplier / 100d;

            return (int) Math.Floor(damage);
        }

    }
}
