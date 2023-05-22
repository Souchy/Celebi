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

            // apply each effect
            foreach (var pair in effectsWithTargets)
            {
                // apply to each target
                foreach(var target in pair.targets)
                {
                    var currentTargetCell = action.fight.board.GetCells().First(c => c.position == target.position);
                    //gotta use subActionEffect instead of action right? 
                    SubActionEffect subActionEffect = new()
                    {
                        fight = action.fight,
                        caster = action.caster,
                        targetCell = currentTargetCell.entityUid, 
                        parent = action,
                        effect = pair.effect
                    };

                    procTriggers(subActionEffect, pair.effect, pair.targets,
                        new TriggerEvent(TriggerType.OnEffect, TriggerOrderType.Before, pair.effect)
                    );

                    IEffectScript script = pair.effect.GetScript(); // todo find script from  pair.effect.schema.GetType
                    var returnValue = script.apply(subActionEffect, target, pair.targets);

                    // TODO how do we deal with the returnValue? 
                    // -> well i think the root effect cannot be used obviously
                    // -> but the returnValue can be useed if the effect is an implementation of IValue for example.
                    // -> or if the effect is a child of a math MetaEffect
                    procTriggers(subActionEffect, pair.effect, pair.targets,
                        new TriggerEvent(TriggerType.OnEffect, TriggerOrderType.After, pair.effect)
                    );
                }
            }
        }
        public static void procTriggers(IAction action, IEffect effect, IEnumerable<IBoardEntity> area, TriggerEvent triggerEvent)
        {
            foreach (IStatusInstance status in action.fight.statuses.Values.SelectMany(sc => sc.instances))
            {
                var triggeredEffects = status.checkTriggers(action, triggerEvent); // effect, area); // orderType, e);
                foreach (var e in triggeredEffects)
                {
                    //e.apply(action, area);
                    applyEffectContainer(action, e); //, need the position to apply right? );
                }
            }
        }
    }
}
