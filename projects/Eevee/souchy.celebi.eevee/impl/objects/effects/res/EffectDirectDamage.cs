using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.res;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;

namespace souchy.celebi.eevee.impl.objects.effects.res
{
    public class EffectDirectDamage : Effect, IEffectDirectDamage
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();


        private EffectDirectDamage() { }
        private EffectDirectDamage(IID id) : base(id) { }
        public static IEffectDirectDamage Create() => new EffectDirectDamage(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            if (action is not IActionSpell) return null;
            IActionSpell act = (IActionSpell) action;

            var creaSource = fight.creatures.Get(act.caster);
            var creaTarget = fight.board.getCreatureOnCell(act.targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetStats(action, trigger);
            var targetStats = creaTarget.GetStats(action, trigger);

            var dist = creaSource.position.distanceManhattan(creaTarget.position);

            // apply affinities + resistances
            IStatSimple aff = sourceStats.Get<IStatSimple>(Element.value.GetAffinity());
            IStatSimple affg = sourceStats.Get<IStatSimple>(Affinity.Damage);
            IStatSimple affdist;

            IStatSimple res = targetStats.Get<IStatSimple>(Element.value.GetResistance());
            IStatSimple resg = targetStats.Get<IStatSimple>(Resistance.Damage);
            IStatSimple resdist;

            // distance
            if (dist > 1)
            {
                affdist = sourceStats.Get<IStatSimple>(Affinity.Distance);
                resdist = targetStats.Get<IStatSimple>(Resistance.Distance);
            }
            // melee
            else
            {
                affdist = sourceStats.Get<IStatSimple>(Affinity.Melee);
                resdist = targetStats.Get<IStatSimple>(Resistance.Melee);
            }

            var damage = Value.value;

            damage *= (100 + aff.value) / 100;
            damage *= (100 + affg.value) / 100;
            damage *= (100 + affdist.value) / 100;

            damage *= (100 - res.value) / 100;
            damage *= (100 - resg.value) / 100;
            damage *= (100 - resdist.value) / 100;

            var compiled = new EffectResultDamage(damage);
            return compiled;
        }
    }
}
