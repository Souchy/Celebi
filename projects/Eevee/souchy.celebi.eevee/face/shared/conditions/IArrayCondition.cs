using souchy.celebi.eevee.face.shared.conditions;

namespace souchy.celebi.eevee.face.conditions
{
    public interface IArrayCondition<T> : ICondition
    {
        public bool allowed { get; set; }
        public T[] values { get; set; }
    }
}
