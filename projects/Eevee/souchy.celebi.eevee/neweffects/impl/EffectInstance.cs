using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.eevee.neweffects
{
    public class EffectInstance : /*Effect,*/ IEffectInstance
    {
        public ObjectId fightUid { get; set; }
        public ObjectId entityUid { get; set; }
        public ObjectId effectPermanentUid { get; init; }
        public ObjectId sourceCreatureUid { get; init; }
        public IEnumerable<ObjectId> targetUids { get; init; }

        /// <summary>
        /// often could be an AddStats schema which contains a Stats object based on ap reduction done, stats stealing, etc.
        /// </summary>
        public IEffectSchema SchemaInstance { get; set; } 
        

        public EffectInstance(IEffectPermanent ep, ObjectId sourceCreatureUid, IEnumerable<IBoardEntity> targets)
        {
            this.effectPermanentUid = ep.entityUid;
            this.sourceCreatureUid = sourceCreatureUid;
            this.targetUids = targets.Select(t => t.entityUid);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //public override IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => this.GetFight().effects.Get(i));
    }
}
