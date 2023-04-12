using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.shared.triggers;

namespace Espeon.souchy.celebi.eeveeimpl.controllers
{
    public class Actions
    {
        public void castSpell(IActionSpell action) //IID source,  IID target, IID spellId)
        {
            ISpellModel s = default;
            ICreature sourceCrea = default;
            IFight fight = default;

            // check costs
            //foreach (ICost cost in s.costs)
            //{
            //    if (source.stats.get<IStatResource>(cost.resource).current < cost.value)
            //    {
            //        return;
            //    }
            //}
            // check line of sight
            // check target cell filter

            // spend costs
            //foreach (ICost cost in s.costs)
            //{
            //    source.stats.get<IStatResource>(cost.resource).current -= cost.value;
            //}
            EffectResultPipeline pipeline = new EffectResultPipeline();

            TriggerEvent trigger = new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.Before);
            // trigger before effects
            foreach (IEffect effect in s.GetEffects())
            {
                pipeline.triggeredBefore.Add(effect.compile(fight, action, trigger)); //source, target, spellId);
            }
            // apply spell effects
            foreach (IEffect effect in s.GetEffects())
            {
                pipeline.children.Add(effect.compile(fight, action, trigger)); //source, target, spellId);
            }
            // trigger after effects
            foreach (IEffect effect in s.GetEffects())
            {
                pipeline.triggeredBefore.Add(effect.compile(fight, action, trigger)); //source, target, spellId);
            }
        }

        public void moveTo(IID sourceCreature, IPosition target)
        {
        }
    }
}