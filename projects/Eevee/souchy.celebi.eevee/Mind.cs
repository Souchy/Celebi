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
using souchy.celebi.eevee.impl.shared.triggers.schemas;

namespace souchy.celebi.eevee
{
    public enum EffectParentChildrenOrder
    {
        ParentBefore,
        ChildrenBefore
    }
    public static class Mind
    {

        public static void applyEffectContainer(IAction action, IEffectsContainer container) //, IPosition targetPosition)
        {
            IPosition targetPosition = action.fight.cells.Get(action.targetCell).position;

            var effectInstances = container.GetEffects()
                .Select(e =>
                {
                    var targets = e.GetPossibleBoardTargets(action, targetPosition);
                    return
                    (
                        effect: EffectInstance.Create(action.fight.entityUid, e), //EffectInstance.Create(action.fight.entityUid, (IEffectPermanent) e, action.caster, targets),
                        targets: targets
                    );
                });

            // apply each effect
            foreach (var pair in effectInstances) //effectsWithTargets)
            {
                //TODO: use EffectInstance
                var sub = new SubActionEffect(action)
                {
                    effect = EffectInstance.Create(action.fight.entityUid, pair.effect),
                    boardTargets = pair.targets,
                };
                // apply to each target
                applyEffectZone(sub);
            }
        }

        public static void applyEffectZone(SubActionEffect parentAction) //, IEffect effect, IEnumerable<IBoardEntity> targets)
        {
            // apply to each target
            foreach (var target in parentAction.boardTargets)
            {
                var currentTargetCell = parentAction.fight.board.GetCells().First(c => c.position.equals(target.position));

                //TODO: make a copy of the effect
                SubActionEffectTarget subActionEffect = new(parentAction)
                {
                    //fight = parentAction.fight,
                    //caster = parentAction.caster,
                    //parent = parentAction,
                    targetCell = currentTargetCell.entityUid,
                    effect = EffectInstance.Create(parentAction.fight.entityUid, parentAction.effect),
                    //depthLevel = parentAction.depthLevel // each target has the same level as the effect that applies to all of them
                };

                var order = EffectParentChildrenOrder.ParentBefore; // put this in Effect
                if (order == EffectParentChildrenOrder.ParentBefore)
                {
                    applyEffect(parentAction, subActionEffect, target);
                    applyChildren(parentAction, subActionEffect);
                }
                else
                if (order == EffectParentChildrenOrder.ChildrenBefore)
                {
                    applyChildren(parentAction, subActionEffect);
                    applyEffect(parentAction, subActionEffect, target);
                }

            }
        }
        
        private static void applyChildren(SubActionEffect parentAction, SubActionEffectTarget subActionEffect)
        {
            /*
            2023-11-28: Dont need the 'IStatusApplicationScript' check anymore since we put Status effects 
            inside another data list in ICreateStatusSchema, separate from the children effects. Meaning we can have both.
            */
            // sub effects -> apply child effects to each target 
            applyEffectContainer(subActionEffect, parentAction.effect);
        }

        private static void applyEffect(SubActionEffect parentAction, SubActionEffectTarget subActionEffect, IBoardEntity target)
        {

            checkTriggers(subActionEffect,
                    new TriggerEvent(TriggerType.TriggerOnEffectCast, TriggerOrderType.Before)
                );

            IEffectScript script = parentAction.effect.GetScript(); // todo find script from  pair.effect.schema.GetType
            var returnValue = script.apply(subActionEffect, target, parentAction.boardTargets);

            // TODO how do we deal with the returnValue? 
            // -> well i think the root effect's return cannot be used obviously
            // -> but the returnValue can be used if the effect is an implementation of IValue for example.
            // -> or if the effect is a child of a math MetaEffect
            checkTriggers(subActionEffect,
                new TriggerEvent(TriggerType.TriggerOnEffectCast, TriggerOrderType.After)
            );


            // set the return value associated with effect in the caster's temporaries/contextuals?
            var caster = parentAction.fight.creatures.Get(parentAction.caster);
            //caster.GetNaturalStats().Set()
            // caster.contextuals.set(action.effect, returnValue)
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
            // TODO we dont want to trigger the same StatusContainer twice if it contains 2 StatusInstances with triggers right?
            //          Maybe... bc we can use the MergeStrategy to allow only 1 StatusInstance if we dont want to trigger twice
            foreach (IStatusInstance status in parentAction.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                var statusAction = new SubActionStatus(parentAction)
                {
                    statusInstance = status
                };

                // Check status triggers
                var triggeredEffects = status.checkTriggers(statusAction, triggerEvent);
                // Apply every triggered effect
                foreach (IEffectInstance triggeredEffect in triggeredEffects)
                {
                    IPosition targetPosition = statusAction.fight.cells.Get(statusAction.targetCell).position;
                    var targets = triggeredEffect.GetPossibleBoardTargets(statusAction, targetPosition);
                    var sub = new SubActionEffect(statusAction)
                    {
                        effect = EffectInstance.Create(parentAction.fight.entityUid, triggeredEffect),
                        boardTargets = targets
                    };
                    applyEffectZone(sub);
                }
            }
        }

    }
}
