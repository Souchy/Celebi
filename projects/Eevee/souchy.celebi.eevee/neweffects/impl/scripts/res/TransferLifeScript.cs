using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.neweffects.impl.effects.res
{
    public class TransferLifeScript : IEffectScript
    {
        public Type SchemaType => typeof(TransferLife);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var creaSource = action.fight.creatures.Get(action.caster);
            var creaTarget = (ICreature) currentTarget;
            var sourceStats = creaSource.GetTotalStats(action);
            var targetStats = creaTarget.GetTotalStats(action);

            // props
            TransferLife props = action.effect.GetProperties<TransferLife>();
            var sourceCurrLife = sourceStats.Get<IStatSimple>(Resource.Life);

            // calc transfer and heal
            var toTransfer = (int) (sourceCurrLife.value * props.value / 100d);

            var toHeal = DamageUtil.calculateHeal(sourceStats, targetStats, toTransfer, applyOffensiveStats: false);

            // limit heal to max life
            var targetCurrLife = sourceStats.Get<IStatSimple>(Resource.Life);
            var targetMaxLife = sourceStats.Get<IStatSimple>(Resource.LifeMax);
            toHeal = Math.Min(toHeal, targetMaxLife.value - targetCurrLife.value);

            // apply
            sourceCurrLife.value -= toTransfer;
            targetCurrLife.value += toHeal;

            // trigger heal/gainlife + loselife
            // guess effects like TransferLife should have tags like HealLife that triggers can use?

            return null;
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
}
