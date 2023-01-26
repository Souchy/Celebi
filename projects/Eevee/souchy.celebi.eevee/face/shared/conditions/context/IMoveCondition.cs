using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions;

namespace souchy.celebi.eevee.face.conditions.context
{
    public interface IMoveCondition : ICondition
    {
        public MoveType moveType { get; set; }
    }
}
