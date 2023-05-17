using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.neweffects.face
{
    public interface IEffect : IEntityModeled, IEffectsContainer
    {
        public IEffectSchema Schema { get; set; }

        public ICondition SourceCondition { get; set; }
        public ICondition TargetFilter { get; set; }
        /// <summary>
        /// Acquired targets with this zone
        /// </summary>
        public IZone TargetAcquisitionZone { get; set; }
        public IEntityList<ITrigger> Triggers { get; set; }


        public T GetProperties<T>() => (T) Schema;
        //public IEffectModel GetModel() => Eevee.models.effectModels.Values.First(m => m.modelUid == this.modelUid);
        /// <summary>
        /// Get unfiltered entities in this effect's area
        /// </summary>
        public IEnumerable<IBoardEntity> GetPossibleBoardTargets(IFight fight, IPosition targetCell);
        /// <summary>
        /// Copy basic properties to passed effect. (not model nor model-specific properties)
        /// </summary>
        public void CopyBasicTo(IEffectInstance e);
    }


}
