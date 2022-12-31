using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntity : IDisposable
    {
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }
    }
}