using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IStatusModel : IEntity, IEffectsContainer
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public IValue<int> delay { get; set; }
        public IValue<int> duration { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(entityUid);
    }
}
