using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.neweffects.impl.effects.move.teleport
{
    public class TeleportTargetToScript : IEffectScript
    {
        public Type SchemaType => typeof(TeleportTargetTo);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            TeleportTargetTo props = action.effect.GetProperties<TeleportTargetTo>();
            //props.MoveTargetZone
            


            throw new NotImplementedException();
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
}
