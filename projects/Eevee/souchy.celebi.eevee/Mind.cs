using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.shared.triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.neweffects.face;
using System.ComponentModel;
using souchy.celebi.eevee.neweffects;
using static System.Collections.Specialized.BitVector32;

namespace souchy.celebi.eevee
{
    public static class Mind
    {

        public static void applyEffectContainer(IAction action, IEffectsContainer container) //, IPosition targetPosition)
        {
            IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;

            // acquire targets for each effects before applying them
            var effectsWithTargets = container.GetEffects()
                .Select(e => (
                    effect: e,
                    targets: e.GetPossibleBoardTargets(action.fight, targetPosition)
                ));


            var effectInstances = container.GetEffects()
                .Select(e =>
                    new EffectInstance(null, action.caster, e.GetPossibleBoardTargets(action.fight, targetPosition))
                );


            // apply each effect
            foreach (var pair in effectsWithTargets)
            {
                //TODO: use EffectInstance
                var sub = new SubActionEffect()
                {
                    fight = action.fight,
                    caster = action.caster,
                    targetCell = action.targetCell,
                    parent = action,
                    effect = pair.effect, //child,
                    boardTargets = pair.targets,
                    depthLevel = action.depthLevel + 1 
                };
                // apply to each target
                applyEffect(sub); //, pair.effect, pair.targets);
            }
        }

        public static void applyEffect(SubActionEffect parentAction) //, IEffect effect, IEnumerable<IBoardEntity> targets)
        {
            // apply to each target
            foreach (var target in parentAction.boardTargets)
            {
                var currentTargetCell = parentAction.fight.board.GetCells().First(c => c.position == target.position);

                //TODO: make a copy of the effect
                SubActionEffectTarget subActionEffect = new()
                {
                    fight = parentAction.fight,
                    caster = parentAction.caster,
                    targetCell = currentTargetCell.entityUid,
                    parent = parentAction,
                    effect = parentAction.effect, // TODO MAKE A COPY
                    depthLevel = parentAction.depthLevel // each target has the same level as the effect that applies to all of them
                };

                checkTriggers(subActionEffect,
                    new TriggerEvent(TriggerType.TriggerOnEffectCast, TriggerOrderType.Before, parentAction)
                );

                IEffectScript script = parentAction.effect.GetScript(); // todo find script from  pair.effect.schema.GetType
                var returnValue = script.apply(subActionEffect, target, parentAction.boardTargets);

                // TODO how do we deal with the returnValue? 
                // -> well i think the root effect's return cannot be used obviously
                // -> but the returnValue can be used if the effect is an implementation of IValue for example.
                // -> or if the effect is a child of a math MetaEffect
                checkTriggers(subActionEffect,
                    new TriggerEvent(TriggerType.TriggerOnEffectCast, TriggerOrderType.After, parentAction)
                );


                // set the return value associated with effect in the caster's temporaries/contextuals?
                var caster = parentAction.fight.creatures.Get(parentAction.caster);
                //caster.GetNaturalStats().Set()
                // caster.contextuals.set(action.effect, returnValue)

                // sub effects -> apply child effects to each target
                applyEffectContainer(subActionEffect, parentAction.effect);
            }
        }


        /*
        /// <summary>
        /// Checks every status in the game to see if they can be triggered by the action
        /// </summary>
        /// <param name="parentAction">current action to react to</param>
        /// <param name="effect">current effect to react to</param>
        /// <param name="triggerEvent"></param>
        /// <param name="area"></param>
        public static void procTriggersOnEffect(SubActionEffectTarget parentAction, IEnumerable<IBoardEntity> area, TriggerEvent triggerEvent)
        {
            // Check every status
            foreach (IStatusInstance status in parentAction.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                // Check status triggers
                var triggeredEffects = status.checkTriggers(parentAction, triggerEvent); // effect, area); // orderType, e);
                // Apply every triggered effect
                foreach (IEffect triggeredEffect in triggeredEffects)
                {
                    ////e.apply(action, area);
                    //applyEffectContainer(action, e); //, need the position to apply right? );

                    // TODO 4sept23? need to make a sub action i think? also applyEffectContainer doesn't apply the effect itself, only its children
                    IPosition targetPosition = parentAction.fight.cells.Get(parentAction.targetCell).position;
                    var targets = triggeredEffect.GetPossibleBoardTargets(parentAction.fight, targetPosition);
                    //var inst = new EffectInstance(null, parentAction.caster, effect.GetPossibleBoardTargets(parentAction.fight, targetPosition));
                    //var targets = inst.targetUids;
                    var sub = new SubActionEffect()
                    {
                        fight = parentAction.fight,
                        caster = parentAction.caster,
                        targetCell = parentAction.targetCell,
                        parent = parentAction,
                        depthLevel = parentAction.depthLevel + 1,
                        effect = triggeredEffect, 
                        boardTargets = targets
                    };
                    applyEffect(sub);
                }
            }
        }
        */

        public static void checkTriggers(IAction parentAction, TriggerEvent triggerEvent)
        {
            // Check every status
            foreach (IStatusInstance status in parentAction.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                // Check status triggers
                var triggeredEffects = status.checkTriggers(parentAction, triggerEvent);
                // Apply every triggered effect
                foreach (IEffect triggeredEffect in triggeredEffects)
                {
                    IPosition targetPosition = parentAction.fight.cells.Get(parentAction.targetCell).position;
                    var targets = triggeredEffect.GetPossibleBoardTargets(parentAction.fight, targetPosition);
                    var sub = new SubActionEffect()
                    {
                        fight = parentAction.fight,
                        caster = parentAction.caster,
                        targetCell = parentAction.targetCell,
                        parent = parentAction,
                        depthLevel = parentAction.depthLevel + 1,
                        effect = triggeredEffect,
                        boardTargets = targets
                    };
                    applyEffect(sub); 
                }
            }
        }

    }
}
