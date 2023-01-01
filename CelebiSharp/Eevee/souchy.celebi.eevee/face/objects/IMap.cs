using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.objects
{
    public interface IMap : IEntity
    {
        public ICell[] cells { get; set; }
    }
}
