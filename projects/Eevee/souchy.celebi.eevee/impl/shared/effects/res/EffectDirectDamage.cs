using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.effects.status;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.compiledeffects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectDirectDamage : Effect, IEffectDirectDamage
    {
        public IValue<ElementType> element { get; set; }
        public IValue<int> Value { get; set; }


        public EffectDirectDamage() { }
        public EffectDirectDamage(IID id) : base(id) { }
        public static IEffectDirectDamage Create() => new EffectDirectDamage(Eevee.RegisterIID());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            var creaSource = fight.creatures.Get(source);
            var creaTarget = fight.board.getCreatureOnCell(targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetStats();
            var targetStats = creaTarget.GetStats();

            var dist = creaSource.position.distanceManhattan(creaTarget.position);

            // apply affinities + resistances
            IStatSimple aff = sourceStats.get<IStatSimple>(element.value.GetAffinity());
            IStatSimple affg = sourceStats.get<IStatSimple>(StatType.GlobalDamageAffinity);
            IStatSimple affdist;

            IStatSimple res = targetStats.get<IStatSimple>(element.value.GetResistance());
            IStatSimple resg = targetStats.get<IStatSimple>(StatType.GlobalDamageResistance);
            IStatSimple resdist;

            // distance
            if (dist > 1)
            {
                affdist = sourceStats.get<IStatSimple>(StatType.DistanceAffinity);
                resdist = targetStats.get<IStatSimple>(StatType.DistanceResistance);
            } 
            // melee
            else
            {
                affdist = sourceStats.get<IStatSimple>(StatType.MeleeAffinity);
                resdist = targetStats.get<IStatSimple>(StatType.MeleeResistance);
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
