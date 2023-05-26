using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IStatusModel : IEntityModel, IEffectsContainer
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public AssetIID icon { get; set; }
        //public IValue<int> delay { get; set; }
        //public IValue<int> duration { get; set; }
        //public IValue<bool> canBeUnbewitched { get; set; }
        public ObjectId statsId { get; set; }
        /// <summary>
        /// Statuses have a priority category and then are sorted by IID ? or date? 
        /// </summary>
        public IValue<StatusPriorityType> priority { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
        public StatusStats GetStats() => (StatusStats) Eevee.models.stats.Get(statsId);
    }
}
