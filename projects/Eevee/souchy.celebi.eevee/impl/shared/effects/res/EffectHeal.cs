﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.compiledeffects;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.values;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectHeal : Effect, IEffectHeal
    {
        public IValue<ElementType> element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();


        private EffectHeal() { }
        private EffectHeal(IID id) : base(id) { }
        public static IEffectHeal Create() => new EffectHeal(Eevee.RegisterIID<IEffect>());


        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            var creaSource = fight.creatures.Get(source);
            var creaTarget = fight.board.getCreatureOnCell(targetCell);
            if (creaSource == null || creaTarget == null) return null;
            var sourceStats = creaSource.GetStats();
            var targetStats = creaTarget.GetStats();
            var dist = creaSource.position.distanceManhattan(creaTarget.position);

            // apply affinities + resistances
            IStatSimple aff = sourceStats.Get<IStatSimple>(element.value.GetAffinity());
            IStatSimple affh = sourceStats.Get<IStatSimple>(StatType.HealAffinity);
            IStatSimple affdist;

            IStatSimple resh = targetStats.Get<IStatSimple>(StatType.HealResistance);

            // distance
            if (dist > 1)
                affdist = sourceStats.Get<IStatSimple>(StatType.DistanceAffinity);
            // melee
            else
                affdist = sourceStats.Get<IStatSimple>(StatType.MeleeAffinity);

            var heal = Value.value;

            heal *= (100 + aff.value) / 100;
            heal *= (100 + affh.value) / 100;
            heal *= (100 + affdist.value) / 100;
            
            heal *= (100 - resh.value) / 100;


            IStatSimple currentLife = targetStats.Get<IStatSimple>(StatType.Life);
            IStatSimple newLife = (IStatSimple) currentLife.copy(); // new StatSimple(StatType.Life, currentLife.value + heal);
            newLife.value += heal;
            var compiled = new CompiledEffectStat(StatType.Life, newLife);
            return compiled;
        }
    }
}
