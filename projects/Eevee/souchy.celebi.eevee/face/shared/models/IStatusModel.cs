﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IStatusModel : IEntity, IEffectsContainer
    {
        //public IStatSimple delay { get; set; }
        //public IStatSimple duration { get; set; }
        public IValue<int> delay { get; set; }
        public IValue<int> duration { get; set; }
        //public IEntityList<IID> effectIds { get; set; }

    }
}
