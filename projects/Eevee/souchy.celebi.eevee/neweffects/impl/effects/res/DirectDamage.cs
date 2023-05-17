using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using IEffect = souchy.celebi.eevee.neweffects.face.IEffect;

namespace souchy.celebi.eevee.neweffects.impl.effects
{
    public sealed record DirectDamageSchema(ElementType elementType, int baseDamage) : IEffectSchema { }

    public sealed class DirectDamageScript : IEffectScript 
    {
        public Type SchemaType => typeof(DirectDamageSchema);
        public IEffectReturnValue apply(IAction action, IEffect e, IEnumerable<IBoardEntity> targets)
        {
            var props = e.GetProperties<DirectDamageSchema>();

            if (action is not IActionSpell) return null;
            IActionSpell act = (IActionSpell) action;

            var creaSource = action.fight.creatures.Get(act.caster);
            var creaTarget = action.fight.board.GetCreatureOnCell(act.targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetTotalStats(action);
            var targetStats = creaTarget.GetTotalStats(action);
            int dmg = calculateDamage(props, sourceStats, targetStats);

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
            return new IEffectReturnValue(e, dmg);
        }

        public IEffectPreview preview(IAction action, IEffect e, IEnumerable<IBoardEntity> targets)
        {
            var props = e.GetProperties<DirectDamageSchema>();
            throw new NotImplementedException();
        }


        private int calculateDamage(DirectDamageSchema props, IStats sourceStats, IStats targetStats)
        {
            // apply affinities + resistances
            IStatSimple aff = sourceStats.Get<IStatSimple>(props.elementType.GetAffinity());
            IStatSimple affg = sourceStats.Get<IStatSimple>(Affinity.Damage);

            int affdist = targetStats.Get<IStatSimple>(Affinity.Distance).value + targetStats.Get<IStatSimple>(Affinity.Melee).value;

            IStatSimple res = targetStats.Get<IStatSimple>(props.elementType.GetResistance());
            IStatSimple resg = targetStats.Get<IStatSimple>(Resistance.Damage);

            int resdist = targetStats.Get<IStatSimple>(Resistance.Distance).value + targetStats.Get<IStatSimple>(Resistance.Melee).value;

            var damage = props.baseDamage;
            damage *= (100 + aff.value) / 100;
            damage *= (100 + affg.value) / 100;
            damage *= (100 + affdist) / 100;

            damage *= (100 - res.value) / 100;
            damage *= (100 - resg.value) / 100;
            damage *= (100 - resdist) / 100;
            return damage;
        }
    }
}
