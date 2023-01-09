using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.conditions.context
{
    public interface IMoveCondition : ICondition
    {
        public MoveType moveType { get; set; }
    }
}
