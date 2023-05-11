using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.util
{
    public interface IEntityList<T> : IEntity
    {
        public bool allowDuplicates();

        public List<T> Values { get;  }
        public void Add(T t);
        public bool Remove(T t);
        public bool Replace(T t0, T t1);
        public void Move(T t, int indexDelta);
        public void Remove(Predicate<T> predicate);
        public bool Contains(T t);
        public void Clear();
    }
    public interface IEntitySet<T> : IEntityList<T>
    {

    }
}
