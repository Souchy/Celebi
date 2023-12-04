using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;
using souchy.celebi.eevee.statuses;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.impl.util.math;

namespace souchy.celebi.eevee.neweffects.impl.scripts.status
{
    public class CreateStatusCreatureScript : IEffectScript
    {
        public Type SchemaType => typeof(CreateStatusCreature);

        public IEffectReturnValue apply(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var props = action.effect.GetProperties<CreateStatusCreature>();
            // var effects = action.effect.GetEffects();

            var creaSource = action.fight.creatures.Get(action.caster);
            var creaTarget = (ICreature) currentTarget;

            var container = StatusContainer.Create(action.fight.entityUid);
            container.holderEntity = currentTarget.entityUid;
            container.sourceCreature = action.caster;
            container.sourceEffectPermanent = action.effect.entityUid;
            container.statsId = StatusContainerStats.Create().entityUid;
            container.sourceSpellModel = action.getClosestSpellSource().Value;

            var statusInstance = StatusInstance.Create(action.fight.entityUid);
            // TODO create Effect instances
            foreach(var effect in props.GetEffects()) //action.effect.GetEffects())
            {
                IEffectInstance effectInst = EffectInstance.Create(action.fight.entityUid, effect); //EffectInstance.Create(action.fight.entityUid, (IEffectPermanent) effect, action.caster, allTargetsInZone);
                // TODO some effects need variable Schemas, ex steal 150 to 170 stats, modify the schema instance.
                // Consume variance 
                Variance.consumeVariance(effectInst.Schema);

                // Some effects need to consume variance when applying them to a Status ((AddStats))
                // Y'a aussi des effets comme retrait -> faut que tu applique puis créé un status à partir de ça.
                // I.Ex.: ReduceAp.apply() {  int ap = rnd(). new status(addstats(ap)); }

                statusInstance.EffectIds.Add(effectInst.entityUid);
            }
            container.instances.Add(statusInstance);

            creaTarget.statuses.Add(container.entityUid);

            return null;
        }

        public IEffectPreview preview(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            return null;
        }
    }
}
