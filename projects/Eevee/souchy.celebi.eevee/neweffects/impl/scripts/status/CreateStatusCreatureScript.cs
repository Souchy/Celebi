using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.statuses;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.enums.characteristics.other;

namespace souchy.celebi.eevee.neweffects.impl.scripts.status
{
    public class CreateStatusCreatureScript : IEffectScript, IStatusApplicationScript
    {
        public Type SchemaType => typeof(CreateStatusCreature);

        public IEffectReturnValue apply(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var props = action.effect.GetProperties<CreateStatusCreature>();
            var effects = action.effect.GetEffects();
            var creaSource = action.fight.creatures.Get(action.caster);
            var creaTarget = (ICreature) currentTarget;

            var container = StatusContainer.Create(action.fight.entityUid);
            container.holderEntity = currentTarget.entityUid;
            container.sourceCreature = action.caster;
            container.sourceEffectPermanent = action.effect.entityUid;
            container.statsId = StatusContainerStats.Create().entityUid;
            container.sourceSpellModel = action.getClosestSpellSource().Value;

            var inst = StatusInstance.Create(action.fight.entityUid);
            // TODO create Effect instances
            foreach(var effect in action.effect.GetEffects())
            {
                IEffectInstance effectInst = EffectInstance.Create(action.fight.entityUid, effect); //EffectInstance.Create(action.fight.entityUid, (IEffectPermanent) effect, action.caster, allTargetsInZone);
                // TODO some effects need variable Schemas, ex steal 150 to 170 stats, modify the schema instance.

                inst.EffectIds.Add(effectInst.entityUid);
            }
            container.instances.Add(inst);

            creaTarget.statuses.Add(container.entityUid);

            return null;
        }

        public IEffectPreview preview(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            return null;
        }
    }
}
