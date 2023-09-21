using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.neweffects.impl.util;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.neweffects.impl.effects.res
{
    public abstract class AbstractDamageScript : IEffectScript
    {
        public abstract Type SchemaType { get; }

        protected abstract bool appliesOffensiveStats { get; }
        protected abstract bool appliesDefensiveStats { get; }
        public IEffectReturnValue apply(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            // Getting TotalStats returns a copy object
            var creaSource = action.fight.creatures.Get(action.caster);
            var sourceStatsCopy = creaSource.GetTotalStats(action);

            // Getting TotalStats returns a copy object
            var creaTarget = (ICreature) currentTarget;
            var targetStatsCopy = creaTarget.GetTotalStats(action);

            // Calc damage
            var props = action.effect.GetProperties<AbstractDamageSchema>();
            int dmg = DamageUtil.calculateDamage(sourceStatsCopy, targetStatsCopy, props, appliesOffensiveStats, appliesDefensiveStats);

            // Calc shield
            var shield = targetStatsCopy.Get<IStatSimple>(Resource.Shield);
            var remainingDmgAfterShield = dmg;

            var startShield = shield.value;
            shield.value -= dmg;
            shield.value = Math.Max(0, shield.value);
            var shieldLost = startShield - shield.value;
            remainingDmgAfterShield += -shieldLost;

            // Calc life
            var life = targetStatsCopy.Get<IStatSimple>(Resource.Life);
            var lifeLost = remainingDmgAfterShield;
            life.value += -lifeLost;

            // update actual stats
            var targetStats = creaTarget.GetNaturalStats();
            var sourceStats = creaSource.GetNaturalStats();
            targetStats.Get<IStatSimple>(Resource.Shield).value = shield.value;
            targetStats.Get<IStatSimple>(Resource.Life).value = life.value;
            targetStats.Get<IStatSimple>(Contextual.DamageTaken).value += (shieldLost + lifeLost);
            sourceStats.Get<IStatSimple>(Contextual.DamageDone).value += (shieldLost + lifeLost);
            targetStats.Get<IStatSimple>(Contextual.CountHitsTaken).value += 1;
            sourceStats.Get<IStatSimple>(Contextual.CountHitsGiven).value += 1;
            
            //var compiled = new EffectPreviewDamage(damage);
            //return compiled;
            //return new IEffectReturnValue(e, dmg);

            // Set results
            var result = new EffectTargetResourceResult();
            action.result = result;
            if (shieldLost != 0) 
                result.resources[Resource.Shield] = -shieldLost;
            if (lifeLost != 0) 
                result.resources[Resource.Life] = -lifeLost;

            return null;
        }

        public IEffectPreview preview(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var props = action.effect.GetProperties<DirectDamage>();
            throw new NotImplementedException();
        }


    }
}
