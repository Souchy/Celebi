using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntityModeled : IEntity
    {
        public IID modelUid { get; set; }
    }
}
