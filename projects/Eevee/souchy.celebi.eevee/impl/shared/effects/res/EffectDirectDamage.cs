using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.compiledeffects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectDirectDamage : Effect, IEffectDirectDamage
    {
        public IValue<ElementType> element { get; set; }
        public IValue<int> Value { get; set; }

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            var creaSource = fight.creatures.Get(source);
            var creaTarget = fight.board.getCreatureOnCell(targetCell);
            if (creaSource == null || creaTarget == null) return null;

            var sourceStats = creaSource.GetStats();
            var targetStats = creaTarget.GetStats();

            var dist = creaSource.position.distanceManhattan(creaTarget.position);

            // apply affinities + resistances
            IStatSimple aff = sourceStats.get<IStatSimple>(element.Value.GetAffinity());
            IStatSimple affg = sourceStats.get<IStatSimple>(StatType.GlobalDamageAffinity);
            IStatSimple affdist;

            IStatSimple res = targetStats.get<IStatSimple>(element.Value.GetResistance());
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

            var damage = this.Value.Value; 

            damage *= (100 + aff.Value) / 100;
            damage *= (100 + affg.Value) / 100;
            damage *= (100 + affdist.Value) / 100;

            damage *= (100 - res.Value) / 100;
            damage *= (100 - resg.Value) / 100;
            damage *= (100 - resdist.Value) / 100;

            var compiled = new CompiledDamage(damage);
            return compiled;
        }
    }
}
