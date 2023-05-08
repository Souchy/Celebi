﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.umbreonUnitTests
{
    public class TestEntity : IEntity
    {
        public IID entityUid { get; set; } = Eevee.RegisterIID<IEntity>();

        public void Dispose()
        {
            Eevee.DisposeIID<IEntity>(entityUid);
        }
    }
}
