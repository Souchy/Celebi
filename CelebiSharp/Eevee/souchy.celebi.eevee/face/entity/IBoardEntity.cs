using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.face.entity
{
    public interface IBoardEntity : IEntityModeled
    {
        public IPosition position { get; init; }
        public List<IStatus> statuses { get; init; }
    }
}