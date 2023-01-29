namespace souchy.celebi.eevee.face.util
{
    public interface IUIdGenerator
    {
        public IID next();
        public bool take(IID id);
        public void dispose(IID id);
    }
}
