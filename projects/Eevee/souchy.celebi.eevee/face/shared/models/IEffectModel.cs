using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IEffectModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
    }
}
