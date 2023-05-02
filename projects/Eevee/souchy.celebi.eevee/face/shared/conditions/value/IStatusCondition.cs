using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface IStatusCondition : IStatCondition //: IIntCondition
    {
        public int statusModelId { get; set; }
    }
}
