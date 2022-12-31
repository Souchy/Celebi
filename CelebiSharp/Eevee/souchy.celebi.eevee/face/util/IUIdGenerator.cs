namespace souchy.celebi.eevee.face.util
{
    public interface IUIdGenerator
    {

        public IID next();

        public void dispose(IID i);

    }
}
