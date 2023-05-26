using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.models
{
    /// <summary>
    /// One StatusModel creates a StatusInstance
    /// That StatusInstance can have its own delay, duration, ...
    /// When added to a BoardEntity, it merges:
    ///     - if there is no StatusContainer for this, create a new one
    ///     - if there is a StatusContainer corresponding, add the instance to it
    ///         - merge strategies will choose what happens: 
    ///             - new instance replaces an older one (x stack limit)
    ///             - new instance is ignored (x stack limit)
    ///             - new instance is added (no stack limit)
    /// </summary>
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
        public StatusModelStats GetStats() => (StatusModelStats) Eevee.models.stats.Get(statsId);
    }
}
