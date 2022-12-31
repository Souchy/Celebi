using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.conditions
{
    public interface IResourceCondition : IIntCondition
    {
        public int statId { get; set; }
    }
}
