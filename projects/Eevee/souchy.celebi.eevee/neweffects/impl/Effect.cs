using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl
{
    public abstract class Effect : IEffect
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IEffectSchema properties { get; set; }
        public ICondition SourceCondition { get; set; }
        public ICondition TargetFilter { get; set; }
        public IZone TargetAcquisitionZone { get; set; }
        public IEntityList<ITrigger> Triggers { get; set; }
        public IEntityList<ObjectId> effectIds { get; set; }

        public void CopyBasicTo(IEffectInstance e)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<eevee.face.objects.IEffect> GetEffects()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBoardEntity> GetPossibleBoardTargets(IFight fight, IPosition targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
