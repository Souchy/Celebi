using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.eevee.neweffects
{
    public class EffectInstance : Effect, IEffectInstance
    {
        public ObjectId fightUid { get; set; }

        public static IEffectInstance Create(ObjectId fightUid, IEffect e)
        {
            var inst = new EffectInstance() { 
                fightUid = fightUid,
                entityUid = Eevee.RegisterIIDTemporary()
            };
            e.CopyBasicTo(inst);
            return inst;
        }


        public override IEnumerable<IEffect> GetEffects() 
            => this.EffectIds.Values.Select(i => this.GetFight().effects.Get(i));
    }

    /*
    public class EffectInstance :  IEffectInstance, // Effect
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
        

        private EffectInstance(ObjectId fightUid, ObjectId entityUid, IEffectPermanent ep, ObjectId sourceCreatureUid, IEnumerable<IBoardEntity> targets)
        {
            this.fightUid = fightUid;
            this.entityUid = entityUid;
            this.effectPermanentUid = ep.entityUid;
            this.sourceCreatureUid = sourceCreatureUid;
            this.targetUids = targets.Select(t => t.entityUid);
        }

        public static IEffectInstance Create(ObjectId fightUid, IEffectPermanent ep, ObjectId sourceCreatureUid, IEnumerable<IBoardEntity> targets)
        {
            var inst = new EffectInstance(fightUid, Eevee.RegisterIIDTemporary(), ep, sourceCreatureUid, targets);
            inst.GetFight().effects.Add(inst.entityUid, inst);
            return inst;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //public override IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => this.GetFight().effects.Get(i));
    }
    */
}
