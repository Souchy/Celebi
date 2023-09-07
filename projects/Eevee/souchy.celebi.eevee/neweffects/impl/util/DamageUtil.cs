﻿using souchy.celebi.eevee.enums.characteristics.creature;
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