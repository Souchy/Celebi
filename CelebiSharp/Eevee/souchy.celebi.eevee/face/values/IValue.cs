namespace souchy.celebi.eevee.values
{
    public interface IValue<T>
    {
        public T get();

        public void set(T val);
    }
}