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
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.face.objects.statuses;
using Espeon.souchy.celebi.espeon.eevee.impl.controllers;
using souchy.celebi.eevee.impl.shared;
using System;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;
using Microsoft.AspNetCore.Hosting;

namespace Espeon.souchy.celebi.eeveeimpl.controllers
{
    public class Actions
    {
        // only on client
        public void previewSpell(IActionSpell action)
        {

        }

        // only on server
        public void castSpell(IActionSpell action) //IID source,  IID target, IID spellId)
        {
            ISpell spell = action.fight.spells.Get(action.spell);
            ISpellModel spellModel = spell.GetModel();
            ICreature sourceCrea = action.fight.creatures.Get(action.caster);
            IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;
            var sourceStats = sourceCrea.GetStats(action); //, new TriggerEvent(TriggerType.CompileStats, TriggerOrderType.Before));
            
            // check costs
            foreach (var cost in spellModel.costs)
            {
                if(cost.Value > sourceStats.Get<IStatSimple>(cost.Key).value)
                {
                    return;
                }
            }
            // check line of sight
            // check target cell filter

            // spend costs
            foreach (var cost in spellModel.costs)
            {
                sourceStats.Get<IStatSimple>(cost.Key).value -= cost.Value;
            }

            EffectResultPipeline pipeline = new EffectResultPipeline();

            TriggerEvent triggerBefore= new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.Before);
            TriggerEvent triggerApply = new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.Apply);
            TriggerEvent triggerAfter = new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.After);

            applyEffectContainer(action, spellModel); //, targetPosition);

            //// trigger before effects
            //foreach (IEffect effect in s.GetEffects())
            //{
            //    var result = effect.apply(fight, action, triggerBefore);
            //    if(result != null)
            //        pipeline.triggeredBefore.Add(result); 
            //}
            //// apply spell effects
            //foreach (IEffect effect in s.GetEffects())
            //{
            //    var result = effect.apply(fight, action, triggerApply);
            //    if (result != null) 
            //        pipeline.children.Add(result); 
            //}
            //// trigger after effects
            //foreach (IEffect effect in s.GetEffects())
            //{
            //    var result = effect.apply(fight, action, triggerAfter);
            //    if (result != null) 
            //        pipeline.triggeredBefore.Add(result);
            //}
            //pipeline.apply(fight);
        }

        public static void applyEffectContainer(IAction action, IEffectsContainer container) //, IPosition targetPosition)
        {
            IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;

            //var effectsWithAreas = container.GetEffects().Select(e => (effect: e, area: e.GetBoardTargets(targetPosition))); //.zone.getArea(targetPosition)));
            var effectsWithTargets = container.GetEffects()
                .Select(e => (
                    effect: e,
                    targets: e.GetPossibleBoardTargets(action.fight, targetPosition)
                ));

            foreach (var pair in effectsWithTargets)
            {
                //gotta use subActionEffect instead of action right? 
                SubActionEffect subActionEffect = new () {
                    fight = action.fight,
                    caster = action.caster,
                    targetCell = action.targetCell, //action.targetCell,
                    effect = pair.effect
                };
                procTriggers(subActionEffect, pair.effect, pair.targets, 
                    new TriggerEvent(TriggerType.OnEffect, TriggerOrderType.Before, pair.effect)
                );
                var returnValue = pair.effect.apply(subActionEffect, pair.targets);
                how do we deal with the returnValue?
                procTriggers(subActionEffect, pair.effect, pair.targets, 
                    new TriggerEvent(TriggerType.OnEffect, TriggerOrderType.After, pair.effect)
                );
            }
        }
        public static void procTriggers(IAction action, IEffect effect, IEnumerable<IBoardEntity> area, TriggerEvent triggerEvent)
        {
            foreach (IStatusInstance status in action.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                var triggeredEffects = status.checkTriggers(action, triggerEvent); // effect, area); // orderType, e);
                foreach(var e in triggeredEffects)
                {
                    //e.apply(action, area);
                    applyEffectContainer(action, e); //, need the position to apply right? );
                }
            }
        }

        public void moveTo(IID sourceCreature, IPosition target)
        {
        }
    }
}