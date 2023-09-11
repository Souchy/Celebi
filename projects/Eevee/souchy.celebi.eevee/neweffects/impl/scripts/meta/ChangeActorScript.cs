using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.neweffects.impl.effects.meta
{
    public class ChangeActorScript : IEffectScript
    {
        public Type SchemaType => typeof(ChangeActor);

        public IEffectReturnValue apply(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var newAction = new SubActionEffectTarget()
            {
                caster = currentTarget.entityUid, // reverse the caster source
                targetCell = action.targetCell,   // keep the same cell though i think? 
                effect = action.effect, // TODO make a copy of the effect like in java?
                fight = action.fight,
                parent = action.parent,
                depthLevel = action.depthLevel + 1,
            };

            // apply the children
            Mind.applyEffectContainer(newAction, action.effect);

            return null;
        }

        public IEffectPreview preview(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
}
