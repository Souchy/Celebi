using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.skins
{
    public interface IEffectSkin : IEntity
    {
        public IID vfxOnTarget { get; set; }
        public IID vfxOnSource { get; set; }
        public IID behaviourScript { get; set; }
    }
}
