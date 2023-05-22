using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl
{
    public abstract class Effect : IEffect
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IEffectSchema Schema { get; set; }
        public ICondition SourceCondition { get; set; }
        public ICondition TargetFilter { get; set; }
        public IZone TargetAcquisitionZone { get; set; } = new Zone();
        public IEntityList<ITrigger> Triggers { get; set; } = new EntityList<ITrigger>();
        public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();


        public abstract IEnumerable<IEffect> GetEffects();

        public void CopyBasicTo(IEffectInstance e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBoardEntity> GetPossibleBoardTargets(IFight fight, IPosition targetCell)
        {
            var model = ((IEffect) this).GetModel();
            var boardTargetType = model.BoardTargetType;

            // use board cells + effect acquisitionZone

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
