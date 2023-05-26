using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl.effects.meta
{
    public class ChangeActorScript : IEffectScript
    {
        public Type SchemaType => typeof(ChangeActor);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            var newAction = new SubActionEffect()
            {
                caster = currentTarget.entityUid, // reverse the caster source
                targetCell = action.targetCell,   // keep the same cell though i think? 
                effect = action.effect,
                fight = action.fight,
                parent = action.parent  
            };

            // apply the children
            Mind.applyEffectContainer(newAction, action.effect);

            return null;
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
}
