using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IMap : IEntity
    {
        public ICell[] cells { get; set; }
    }
}
