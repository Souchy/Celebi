using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntityModeled : IEntity
    {
        public IID modelId { get; set; }
    }
}
