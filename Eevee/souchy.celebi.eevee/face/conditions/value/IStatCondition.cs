using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.conditions
{
    public interface IStatCondition : IIntCondition
    {
        public int statId { get; set; }
    }
}
