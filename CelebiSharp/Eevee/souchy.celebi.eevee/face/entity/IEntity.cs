namespace souchy.celebi.eevee.face.entity
{
    public interface IEntity : IDisposable
    {
        public uint entityUid { get; init; }
    }
}