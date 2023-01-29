﻿using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntity : IDisposable
    {
        public IID entityUid { get; set; }

        //public IEventBus GetEventBus() => Eevee.eventBuses[entityUid];

    }
}