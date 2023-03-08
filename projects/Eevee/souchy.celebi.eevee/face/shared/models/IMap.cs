using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IMap : IEntity
    {
        public IID name { get; set; }
        public IVector3[][] teamsStartPositions { get; set; }
        public ICell[] cells { get; set; }
    }
}
