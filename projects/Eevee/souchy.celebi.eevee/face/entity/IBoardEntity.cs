using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.entity
{
    public interface IBoardEntity : IEntityModeled, IFightEntity
    {
        public IPosition position { get; init; }
        public IEntitySet<ObjectId> statuses { get; init; } 

        public Dictionary<ContextType, IContext> contexts { get; set; }


        public IEnumerable<IStatusContainer> GetStatuses() => this.GetFight().statuses.Values.Where(s => statuses.Contains(s.entityUid));

    }
}