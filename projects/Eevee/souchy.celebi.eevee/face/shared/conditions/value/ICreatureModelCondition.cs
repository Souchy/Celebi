using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.shared.conditions.value
{
    public interface ICreatureModelCondition : ICondition //: IIntCondition
    {
        public string creatureModelId { get; set; }
    }
}
