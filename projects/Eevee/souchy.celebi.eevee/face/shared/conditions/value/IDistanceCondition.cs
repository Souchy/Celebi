using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface IDistanceCondition : ICondition //: IIntCondition
    {
        public int distance { get; set; }
    }
}
