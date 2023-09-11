using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.neweffects.face
{
    /// <summary>
    /// An effect instance in the fight, based on an EffectPermanent. <br></br>
    /// Automatically created on spell cast to use through the pipeline and kept in Statuses.
    /// </summary>
    public interface IEffectInstance : IFightEntity //, IEffect
    {

        /// <summary>
        /// Effect reference for subeffects, triggers, conditions, zones, etc.
        /// </summary>
        public ObjectId effectPermanentUid { get; init; }
        public ObjectId sourceCreatureUid { get; init; }
        public IEnumerable<ObjectId> targetUids { get; init; }
        public IEffectSchema SchemaInstance { get; set; }

        public IEffectPermanent GetEffectPermanent() => (IEffectPermanent) Eevee.models.effects.Get(effectPermanentUid);
    }


}
