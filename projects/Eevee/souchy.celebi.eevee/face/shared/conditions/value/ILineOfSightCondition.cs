using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface ILineOfSightCondition : ICondition
    {
        public bool haveLos { get; set; }
    }
}