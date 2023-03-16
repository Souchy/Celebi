using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface IMomentCondition : ICondition
    {
        public MomentType momentType { get; set; }
    }
}
