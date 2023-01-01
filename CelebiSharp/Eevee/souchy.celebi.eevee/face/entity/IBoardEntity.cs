using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.face.entity
{
    public interface IBoardEntity : IEntityModeled
    {
        public IPosition position { get; init; }
        public List<IID> statuses { get; init; }

        public Dictionary<ContextType, IContext> contextsStats { get; set; }
    }
}