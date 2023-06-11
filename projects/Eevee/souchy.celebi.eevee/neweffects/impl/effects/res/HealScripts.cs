using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects.creature;
using souchy.celebi.eevee.neweffects.impl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl.effects.res
{
    public record HealScript : IEffectScript
    {
        public Type SchemaType => typeof(Heal);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var creaSource = action.fight.creatures.Get(action.caster);
            var creaTarget = action.fight.board.GetCreatureOnCell(action.targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetTotalStats(action);
            var targetStats = creaTarget.GetTotalStats(action);
            
            // calc heal
            Heal props = action.effect.GetProperties<Heal>();
            var toHeal = DamageUtil.calculateHeal(sourceStats, targetStats, props.baseHeal, props.element, true, true);

            // limit heal to max life
            var targetCurrLife = sourceStats.Get<IStatSimple>(Resource.Life);
            var targetMaxLife = sourceStats.Get<IStatSimple>(Resource.LifeMax);
            toHeal = Math.Min(toHeal, targetMaxLife.value - targetCurrLife.value);

            // apply
            targetCurrLife.value += toHeal;

            // trigger heal/gainlife
            return null;
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
    public record HealPercentLifeMaxScript : IEffectScript
    {
        public Type SchemaType => typeof(HealPercentLifeMax);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var creaSource = action.fight.creatures.Get(action.caster);
            var sourceStats = creaSource.GetTotalStats(action);
            var creaTarget = (ICreature) currentTarget;
            var targetStats = creaTarget.GetTotalStats(action);

            // calc heal
            HealPercentLifeMax props = action.effect.GetProperties<HealPercentLifeMax>();
            var currLifeMax = props.percentOfWhoseLife == enums.ActorType.Source
                    ? sourceStats.Get<IStatSimple>(Resource.LifeMax)
                    : targetStats.Get<IStatSimple>(Resource.LifeMax);
            var baseHeal = (int) (currLifeMax.value * props.percentHeal / 100d);
            var toHeal = DamageUtil.calculateHeal(sourceStats, targetStats, baseHeal, props.element, true, true);

            // limit heal to max life
            var targetCurrLife = sourceStats.Get<IStatSimple>(Resource.Life);
            var targetMaxLife = sourceStats.Get<IStatSimple>(Resource.LifeMax);
            toHeal = Math.Min(toHeal, targetMaxLife.value - targetCurrLife.value);

            // apply
            targetCurrLife.value += toHeal;

            // trigger heal/gainlife

            return null;
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
    public record HealPercentDamageReceivedByEffectScript : IEffectScript
    {
        public Type SchemaType => typeof(HealPercentDamageReceivedByEffectScript);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
    public record HealPercentDamageDoneByEffectScript : IEffectScript
    {
        public Type SchemaType => typeof(HealPercentDamageDoneByEffect);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
}
