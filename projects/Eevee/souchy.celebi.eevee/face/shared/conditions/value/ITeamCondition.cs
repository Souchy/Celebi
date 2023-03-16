using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface ITeamCondition : ICondition //: IIntCondition
    {
        public TeamRelationType teamType { get; set; }
    }
}
