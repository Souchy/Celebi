using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace UmbreonUnitTests
{
    public class TestEntity : IEntity
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();

        public void Dispose()
        {
            Eevee.DisposeIID(this);
        }
    }
}
