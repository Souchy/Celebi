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

            Todo: Be careful to have a new action going from the spell to the effects

            // apply each effect
            foreach (var pair in effectsWithTargets)
            {
                // apply to each target
                applyEffect(action, pair.effect, pair.targets);
            }
        }

        public static void applyEffect(IAction parentAction, IEffect effect, IEnumerable<IBoardEntity> targets)
        {
            // apply to each target
            foreach (var target in targets)
            {
                var currentTargetCell = parentAction.fight.board.GetCells().First(c => c.position == target.position);
                //gotta use subActionEffect instead of action right? 
                SubActionEffectTarget subActionEffect = new()
                {
                    fight = parentAction.fight,
                    caster = parentAction.caster,
                    targetCell = currentTargetCell.entityUid,
                    parent = parentAction,
                    effect = effect,
                    depthLevel = parentAction.depthLevel // each target has the same level as the effect that applies to all of them
                };

                procTriggers(subActionEffect, effect, targets,
                    new TriggerEvent(TriggerType.OnEffect, TriggerOrderType.Before, effect)
                );

                IEffectScript script = effect.GetScript(); // todo find script from  pair.effect.schema.GetType
                var returnValue = script.apply(subActionEffect, target, targets);

                // TODO how do we deal with the returnValue? 
                // -> well i think the root effect's return cannot be used obviously
                // -> but the returnValue can be used if the effect is an implementation of IValue for example.
                // -> or if the effect is a child of a math MetaEffect
                procTriggers(subActionEffect, effect, targets,
                    new TriggerEvent(TriggerType.OnEffect, TriggerOrderType.After, effect)
                );


                // set the return value associated with effect in the cater's temporaries/contextuals?
                var caster = parentAction.fight.creatures.Get(parentAction.caster);
                //caster.GetNaturalStats().Set()
                // caster.contextuals.set(action.effect, returnValue)

                // sub effects
                // this implementation will break the TargetAcquisition principle i think, but fine for now
                foreach (var child in effect.GetEffects())
                {
                    // Todo: rename SubEffectAction->SubActionEffect and give it its own targets, not the parents (in applyEffectContainer?)
                    var sub = new SubEffectAction()
                    {
                        fight = subActionEffect.fight,
                        caster = subActionEffect.caster,
                        targetCell = subActionEffect.targetCell,
                        parent = subActionEffect,
                        effect = child,
                        parentBoardTargets = targets,
                        depthLevel = parentAction.depthLevel + 1 // maybe there's an error here, we already create a new action at L55 for each target,
                                                                 // so why create an action for the entire effect? hmm
                                                                 // comment above also says it might break the TargetAcquisition principle....
                    };
                    applyEffectContainer(sub, child);
                }
            }
        }


        /// <summary>
        /// Checks every status in the game to see if they can be triggered by the action
        /// </summary>
        /// <param name="action">current action to react to</param>
        /// <param name="effect">current effect to react to</param>
        /// <param name="triggerEvent"></param>
        /// <param name="area"></param>
        public static void procTriggersOnEffect(SubActionEffectTarget action, IEnumerable<IBoardEntity> area, TriggerEvent triggerEvent)
        {
            // Check every status
            foreach (IStatusInstance status in action.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                // Check status triggers
                var triggeredEffects = status.checkTriggers(action, triggerEvent); // effect, area); // orderType, e);
                // Apply every triggered effect
                foreach (var e in triggeredEffects)
                {
                    ////e.apply(action, area);
                    //applyEffectContainer(action, e); //, need the position to apply right? );


                    // TODO 4sept23? need to make a sub action i think? also applyEffectContainer doesn't apply the effect itself, only its children
                    IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;
                    var targets = e.GetPossibleBoardTargets(action.fight, targetPosition);
                    //var inst = new EffectInstance(null, parentAction.caster, effect.GetPossibleBoardTargets(parentAction.fight, targetPosition));
                    //var targets = inst.targetUids;
                    applyEffect(action, e, targets);
                }
            }
        }

        public static void checkTriggers(IAction action, TriggerEvent triggerEvent)
        {
            // Check every status
            foreach (IStatusInstance status in action.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                // Check status triggers
                var triggeredEffects = status.checkTriggers(action, triggerEvent);
                // Apply every triggered effect
                foreach (var e in triggeredEffects)
                {

                    IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;
                    var targets = e.GetPossibleBoardTargets(action.fight, targetPosition);
                    applyEffect(action, e, targets);
                }
            }
        }

    }
}
