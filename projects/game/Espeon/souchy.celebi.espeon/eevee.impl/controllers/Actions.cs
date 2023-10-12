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
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.espeon.eevee.impl.controllers;
using souchy.celebi.eevee.impl.shared;
using System;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;
using Microsoft.AspNetCore.Hosting;
using souchy.celebi.eevee.enums.characteristics.creature;

namespace souchy.celebi.espeon.eevee.impl.controllers
{
    public class Actions
    {
        // only on client
        public void previewSpell(IActionSpell action)
        {

        }

        public void passTurn(IActionPass action)
        {
            IFight fight = action.fight;
            ITimeline timeline = fight.timeline;

            // Check that the current player and current creature match the action
            var currentCreature = timeline.getCurrentCreature();
            var currentPlayer = timeline.getCurrentPlayer();
            if (currentCreature.entityUid != action.caster ||
                currentPlayer.entityUid != action.player.entityUid)
            {
                return;
            }

            // Apply timeline & proc Triggers
            timeline.nextTurn();
        }

        // only on server
        public void castSpell(IActionSpell action) //IID source,  IID target, IID spellId)
        {
            ISpell spell = action.fight.spells.Get(action.spell);
            ISpellModel spellModel = spell.GetModel();
            ICreature sourceCrea = action.fight.creatures.Get(action.caster);
            IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;
            var sourceStats = sourceCrea.GetTotalStats(action); //, new TriggerEvent(TriggerType.CompileStats, TriggerOrderType.Before));

            var spellStats = spell.GetStats(); //.AddAll(spellModel.GetStats());
            var costs = Resource.values
                .Where(p => p.Value.resProp == ResourceProperty.Current)
                .Select(p =>
                    spellStats.Has(p.Key) ? null : spellStats.Get<IStatSimple>(p.Key)
                ).Where(c => c != null);

            // check costs
            foreach (var cost in costs)
            {
                if (!sourceStats.Has(cost.statId)) return;
                if (cost.value > sourceStats.Get<IStatSimple>(cost.statId).value) return;
            }
            // check line of sight
            // check target cell filter

            // spend costs
            foreach (var cost in costs)
            {
                sourceStats.Get<IStatSimple>(cost.statId).value -= cost.value;
            }

            //EffectPreviewPipeline pipeline = new EffectPreviewPipeline();

            TriggerEvent triggerBefore = new TriggerEvent(TriggerType.TriggerOnSpellCast, TriggerOrderType.Before, action);
            //TriggerEvent triggerApply = new TriggerEvent(TriggerType.OnCreatureSpellCast, TriggerOrderType.Apply);
            TriggerEvent triggerAfter = new TriggerEvent(TriggerType.TriggerOnSpellCast, TriggerOrderType.After, action);

            // Check triggers Before
            Mind.checkTriggers(action, triggerBefore);
            // Apply spell
            Mind.applyEffectContainer(action, spellModel);
            // Check triggers After
            Mind.checkTriggers(action, triggerAfter);

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


        public void moveTo(IID sourceCreature, IPosition target)
        {
        }
    }
}
