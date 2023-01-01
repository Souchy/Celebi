using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntityModeled : IFightEntity
    {
        public IID modelId { get; set; }
    }
}
