using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions.creature;

namespace souchy.celebi.eevee.face.shared.conditions.status
{
    public interface IStatusCondition : IStatCondition //: IIntCondition
    {
        public int statusModelId { get; set; }
    }
}
