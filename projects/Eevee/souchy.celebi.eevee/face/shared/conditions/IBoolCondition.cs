using souchy.celebi.eevee.face.shared.conditions;

namespace souchy.celebi.eevee.face.conditions
{
    public interface IBoolCondition : ICondition
    {
        public bool value { get; set; }
    }
}
