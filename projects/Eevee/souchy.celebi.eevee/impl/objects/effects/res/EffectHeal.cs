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
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.values;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.res
{
    public class EffectHeal : Effect, IEffectHeal
    {
        public IValue<ElementType> Element { get; set; } = new Value<ElementType>();
        public IValue<int> Value { get; set; } = new Value<int>();


        private EffectHeal() { }
        private EffectHeal(ObjectId id) : base(id) { }
        public static IEffectHeal Create() => new EffectHeal(Eevee.RegisterIIDTemporary());


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
            var sourceStats = creaSource.GetTotalStats(action); //, trigger);
            var targetStats = creaTarget.GetTotalStats(action); //, trigger);
            var dist = creaSource.position.distanceManhattan(creaTarget.position);

            // apply affinities + resistances
            IStatSimple aff = sourceStats.Get<IStatSimple>(Element.value.GetAffinity());
            IStatSimple affh = sourceStats.Get<IStatSimple>(Affinity.Heal);
            IStatSimple affdist;

            IStatSimple resh = targetStats.Get<IStatSimple>(Resistance.Heal);

            // distance
            if (dist > 1)
                affdist = sourceStats.Get<IStatSimple>(Affinity.Distance);
            // melee
            else
                affdist = sourceStats.Get<IStatSimple>(Affinity.Melee);

            var heal = Value.value;

            heal *= (100 + aff.value) / 100;
            heal *= (100 + affh.value) / 100;
            heal *= (100 + affdist.value) / 100;

            heal *= (100 - resh.value) / 100;


            IStatSimple currentLife = targetStats.Get<IStatSimple>(Resource.Life);
            IStatSimple newLife = (IStatSimple)currentLife.copy(); // new StatSimple(StatType.Life, currentLife.value + heal);
            newLife.value += heal;
            //var compiled = new EffectResultStat(Resource.Life.ID, newLife);
            //return compiled;
            return null;
        }
    }
}
