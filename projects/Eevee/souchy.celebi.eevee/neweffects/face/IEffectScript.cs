using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.face.objects.effectResults;

namespace souchy.celebi.eevee.neweffects.face
{

    public interface IEffectScript 
    {
        public Type SchemaType { get; }
        /// <summary>
        /// Apply the effect to 1 target. Repeated by Mind for all targets in the zone.
        /// </summary>
        /// <param name="action">The Effect Action has a Effect object and an Action parent</param>
        /// <param name="currentTarget">The target to apply to</param>
        /// <param name="allTargetsInZone">All the targets in the zone if we need to refer to them in calculations</param>
        /// <returns></returns>
        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone);
        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone);
    }
}
