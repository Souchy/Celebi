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
using souchy.celebi.eevee.face.objects.stats;

namespace Espeon.souchy.celebi.eeveeimpl.controllers
{
    public class Actions
    {
        public void castSpell(IActionSpell action) //IID source,  IID target, IID spellId)
        {
            ISpellModel s = default;
            ICreature sourceCrea = default;
            IFight fight = default;
            var sourceStats = sourceCrea.GetStats(action, new TriggerEvent(TriggerType.CompileStats, TriggerOrderType.Before));
            
            // check costs
            foreach (var cost in s.costs)
            {
                if(cost.Value > sourceStats.Get<IStatSimple>(cost.Key).value)
                {
                    return;
                }
            }
            // check line of sight
            // check target cell filter

            // spend costs
            foreach (var cost in s.costs)
            {
                sourceStats.Get<IStatSimple>(cost.Key).value -= cost.Value;
            }

            EffectResultPipeline pipeline = new EffectResultPipeline();

            TriggerEvent triggerBefore= new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.Before);
            TriggerEvent triggerApply = new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.Apply);
            TriggerEvent triggerAfter = new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.After);
            // trigger before effects
            foreach (IEffect effect in s.GetEffects())
            {
                var result = effect.compile(fight, action, triggerBefore);
                if(result != null)
                    pipeline.triggeredBefore.Add(result); 
            }
            // apply spell effects
            foreach (IEffect effect in s.GetEffects())
            {
                var result = effect.compile(fight, action, triggerApply);
                if (result != null) 
                    pipeline.children.Add(result); 
            }
            // trigger after effects
            foreach (IEffect effect in s.GetEffects())
            {
                var result = effect.compile(fight, action, triggerAfter);
                if (result != null) 
                    pipeline.triggeredBefore.Add(result);
            }
        }

        public void moveTo(IID sourceCreature, IPosition target)
        {
        }
    }
}