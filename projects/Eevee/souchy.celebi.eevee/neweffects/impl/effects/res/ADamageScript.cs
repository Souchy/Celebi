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

namespace souchy.celebi.eevee.neweffects.impl.effects.res
{
    public abstract class ADamageScript : IEffectScript
    {
        public abstract Type SchemaType { get; }

        protected abstract bool appliesOffensiveStats { get; }
        protected abstract bool appliesDefensiveStats { get; }
        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var creaSource = action.fight.creatures.Get(action.caster);
            var sourceStats = creaSource.GetTotalStats(action);

            var creaTarget = (ICreature) currentTarget;
            var targetStats = creaTarget.GetTotalStats(action);

            var props = action.effect.GetProperties<ADamageSchema>();
            int dmg = DamageUtil.calculateDamage(sourceStats, targetStats, props, appliesOffensiveStats, appliesDefensiveStats);

            var shield = targetStats.Get<IStatSimple>(Resource.Shield);
            var remainingDmgAfterShield = dmg;

            var startShield = shield.value;
            shield.value -= dmg;
            shield.value = Math.Max(0, shield.value);
            remainingDmgAfterShield = startShield - shield.value;

            var life = targetStats.Get<IStatSimple>(Resource.Life);
            life.value -= remainingDmgAfterShield;

            //var compiled = new EffectPreviewDamage(damage);
            //return compiled;
            //return new IEffectReturnValue(e, dmg);
            return null;
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var props = action.effect.GetProperties<DirectDamage>();
            throw new NotImplementedException();
        }


    }
}
