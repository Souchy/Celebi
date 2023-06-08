using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.creature
{
    public interface ICreatureModelCondition : ICondition //: IIntCondition
    {
        public string creatureModelId { get; set; }
    }
}
