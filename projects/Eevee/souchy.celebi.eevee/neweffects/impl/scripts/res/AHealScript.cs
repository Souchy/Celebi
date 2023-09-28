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
    public abstract class AHealScript : IEffectScript
    {
        public abstract Type SchemaType { get; }

        protected abstract bool appliesOffensiveStats { get; }
        protected abstract bool appliesDefensiveStats { get; }

        public IEffectReturnValue apply(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var creaSource = action.fight.creatures.Get(action.caster);
            var creaTarget = (ICreature) currentTarget;
            var sourceStatsCopy = creaSource.GetTotalStats(action);
            var targetStatsCopy = creaTarget.GetTotalStats(action);

            var props = action.effect.GetProperties<AbstractDamageSchema>();
            int dmg = DamageUtil.calculateDamage(sourceStatsCopy, targetStatsCopy, props, appliesOffensiveStats, appliesDefensiveStats);

            //var shield = targetStats.Get<IStatSimple>(Resource.Shield);
            //var remainingDmgAfterShield = dmg;

            //var startShield = shield.value;
            //shield.value -= dmg;
            //shield.value = Math.Max(0, shield.value);
            //remainingDmgAfterShield = startShield - shield.value;

            var life = targetStatsCopy.Get<IStatSimple>(Resource.Life).value;
            var lifeMax = targetStatsCopy.Get<IStatSimple>(Resource.LifeMax).value;
            int lifeGained = Math.Min(dmg, lifeMax - life);
            life += lifeGained;

            // update actual stats
            var targetStats = creaTarget.GetNaturalStats();
            var sourceStats = creaSource.GetNaturalStats();
            targetStats.Get<IStatSimple>(Resource.Life).value = life;
            targetStats.Get<IStatSimple>(Contextual.HealReceived).value += lifeGained;
            sourceStats.Get<IStatSimple>(Contextual.HealDone).value += lifeGained;

            //var compiled = new EffectPreviewDamage(damage);
            //return compiled;

            // Set results
            var result = new EffectTargetResourceResult();
            action.result = result;
            if (lifeGained != 0)
                result.resources[Resource.Life] = lifeGained;

            return new IEffectReturnValue(action.effect, dmg);
        }

        public IEffectPreview preview(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var props = action.effect.GetProperties<DirectDamage>();
            throw new NotImplementedException();
        }

    }
}
