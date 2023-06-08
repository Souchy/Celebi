using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.other
{
    public interface ITeamCondition : ICondition //: IIntCondition
    {
        public TeamRelationType teamType { get; set; }
    }
}
