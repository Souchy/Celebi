using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.res;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;
using System.Collections.Generic;

namespace souchy.celebi.eevee.impl.objects.effects.res
{
    public class EffectDirectDamage : Effect, IEffectDirectDamage
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();


        private EffectDirectDamage() { }
        private EffectDirectDamage(ObjectId id) : base(id) { }
        public static IEffectDirectDamage Create() => new EffectDirectDamage(Eevee.RegisterIIDTemporary());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            if (action is not IActionSpell) return null;
            IActionSpell act = (IActionSpell) action;

            var creaSource = action.fight.creatures.Get(act.caster);
            var creaTarget = action.fight.board.GetCreatureOnCell(act.targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetStats(action);
            var targetStats = creaTarget.GetStats(action);
            int dmg = calculateDamage(sourceStats, targetStats);

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
            return new IEffectReturnValue(this, dmg);
        }

        private int calculateDamage(IStats sourceStats, IStats targetStats)
        {
            // apply affinities + resistances
            IStatSimple aff = sourceStats.Get<IStatSimple>(Element.value.GetAffinity());
            IStatSimple affg = sourceStats.Get<IStatSimple>(Affinity.Damage);

            int affdist = targetStats.Get<IStatSimple>(Affinity.Distance).value + targetStats.Get<IStatSimple>(Affinity.Melee).value;

            IStatSimple res = targetStats.Get<IStatSimple>(Element.value.GetResistance());
            IStatSimple resg = targetStats.Get<IStatSimple>(Resistance.Damage);

            int resdist = targetStats.Get<IStatSimple>(Resistance.Distance).value + targetStats.Get<IStatSimple>(Resistance.Melee).value;

            var damage = Value.value;
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
