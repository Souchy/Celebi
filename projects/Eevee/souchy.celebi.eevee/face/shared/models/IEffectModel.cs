using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models
{
    /// <summary>
    /// This is like the EffectType. it has a modelId and the string ids for instances to use
    /// </summary>
    public interface IEffectModel : IEntityModel
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
    }
}
