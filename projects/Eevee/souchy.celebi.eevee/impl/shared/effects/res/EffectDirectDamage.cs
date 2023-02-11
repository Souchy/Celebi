using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.compiledeffects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectDirectDamage : Effect, IEffectDirectDamage
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();


        private EffectDirectDamage() { }
        private EffectDirectDamage(IID id) : base(id) { }
        public static IEffectDirectDamage Create() => new EffectDirectDamage(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            var creaSource = fight.creatures.Get(source);
            var creaTarget = fight.board.getCreatureOnCell(targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetStats();
            var targetStats = creaTarget.GetStats();

            var dist = creaSource.position.distanceManhattan(creaTarget.position);

            // apply affinities + resistances
            IStatSimple aff = sourceStats.Get<IStatSimple>(Element.value.GetAffinity());
            IStatSimple affg = sourceStats.Get<IStatSimple>(StatType.GlobalDamageAffinity);
            IStatSimple affdist;

            IStatSimple res = targetStats.Get<IStatSimple>(Element.value.GetResistance());
            IStatSimple resg = targetStats.Get<IStatSimple>(StatType.GlobalDamageResistance);
            IStatSimple resdist;

            // distance
            if (dist > 1)
            {
                affdist = sourceStats.Get<IStatSimple>(StatType.DistanceAffinity);
                resdist = targetStats.Get<IStatSimple>(StatType.DistanceResistance);
            } 
            // melee
            else
            {
                affdist = sourceStats.Get<IStatSimple>(StatType.MeleeAffinity);
                resdist = targetStats.Get<IStatSimple>(StatType.MeleeResistance);
            }

            var damage = this.Value.value; 

            damage *= (100 + aff.value) / 100;
            damage *= (100 + affg.value) / 100;
            damage *= (100 + affdist.value) / 100;

            damage *= (100 - res.value) / 100;
            damage *= (100 - resg.value) / 100;
            damage *= (100 - resdist.value) / 100;

            var compiled = new CompiledDamage(damage);
            return compiled;
        }
    }
}
