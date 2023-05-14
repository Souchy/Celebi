using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public class EntitySet<T> : EntityList<T>, IEntitySet<T> //where T : IEntity
    {
        public EntitySet() { }
        public EntitySet(IEnumerable<T> list) : base(list.Distinct()) { }
        public override bool allowDuplicates() => false;
    }

    public class EntityList<T> : List<T>, IEntityList<T> //where T : IEntity
    {

        public const string EventAdd = nameof(Add);
        public const string EventRemove = nameof(Remove);
        public const string EventMove = nameof(Move);
        public const string EventReplace = nameof(Replace);

        [JsonIgnore]
        [BsonId]
        public ObjectId entityUid { get; set; } = Eevee.RegisterIIDTemporary();
        [JsonIgnore]
        public List<T> Values { get => this; }

        public EntityList() { }
        public EntityList(IEnumerable<T> list) {
            this.AddRange(list);
        }

        public virtual bool allowDuplicates() => true; 

        public new void Add(T t)
        {
            if (!this.allowDuplicates() && this.Contains(t))
            {
                throw new ArgumentException($"Duplicate element: {t}.");
            }
            base.Add(t);
            this.GetEntityBus().publish(EventAdd, t);
        }

        public new bool Remove(T t)
        {
            bool removed = base.Remove(t);
            if (removed)
            {
                this.GetEntityBus().publish(EventRemove, t);
                //if (t is IDisposable dis)
                //    dis.Dispose();
            }
            return removed;
        }

        public bool Replace(T t0, T t1)
        {
            bool removed = base.Remove(t0);
            base.Add(t1);
            this.GetEntityBus().publish(EventReplace, t0, t1);
            return removed;
        }

        /// <summary>
        /// Move an element of the list up or down
        /// </summary>
        /// <param name="t"></param>
        /// <param name="indexDelta">Negative to move towards the start of the list (reduce index) or positive to move towards the end of the list</param>
        public void Move(T t, int indexDelta)
        {
            //base.Add(t);
            var index0 = base.IndexOf(t);
            if (index0 == -1) 
                return;
            if (index0 + indexDelta < 0)
                throw new IndexOutOfRangeException();
            bool removed = base.Remove(t);
            if (removed)
            {
                base.Insert(index0 + indexDelta, t);
                this.GetEntityBus().publish(EventMove, t, index0, index0 + indexDelta);
            }
            else
            {
                throw new Exception($"Failed to remove element: {t}");
            }
        }

        public void Remove(Predicate<T> predicate)
        {
            var toRemove = this.Where(p => predicate(p)).ToList();
            foreach (var rem in toRemove)
                this.Remove(rem);
        }

        public new void Clear()
        {
            foreach (var k in this.ToList())
                Remove(k);
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            Clear();
        }
    }
}
