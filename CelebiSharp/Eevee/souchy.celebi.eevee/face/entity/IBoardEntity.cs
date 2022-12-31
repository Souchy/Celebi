﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.face.entity
{
    public interface IBoardEntity : IEntityModeled
    {
        public IPosition position { get; init; }
        public List<IID> statusIds { get; init; }

        public Dictionary<ContextType, IContext> contextsStats { get; set; }
    }
}