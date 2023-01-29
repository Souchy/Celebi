using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.compiledeffects;
using souchy.celebi.eevee.impl.stats;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectHeal : Effect, IEffectHeal
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
            IStatSimple aff = sourceStats.get<IStatSimple>(element.value.GetAffinity());
            IStatSimple affh = sourceStats.get<IStatSimple>(StatType.HealAffinity);
            IStatSimple affdist;

            IStatSimple resh = targetStats.get<IStatSimple>(StatType.HealResistance);

            // distance
            if (dist > 1)
                affdist = sourceStats.get<IStatSimple>(StatType.DistanceAffinity);
            // melee
            else
                affdist = sourceStats.get<IStatSimple>(StatType.MeleeAffinity);

            var heal = Value.value;

            heal *= (100 + aff.value) / 100;
            heal *= (100 + affh.value) / 100;
            heal *= (100 + affdist.value) / 100;
            
            heal *= (100 - resh.value) / 100;


            IStatSimple currentLife = targetStats.get<IStatSimple>(StatType.Life);
            var newLife = new StatSimple(currentLife.value + heal);
            var compiled = new CompiledEffectStat(StatType.Life, newLife);
            return compiled;
        }
    }
}
