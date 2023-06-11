using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects.stats;

namespace souchy.celebi.eevee.face.shared.conditions.creature
{
    public interface IStatsCondition : ICondition
    {
        public IStats conditionStats { get; set; }
    }
}
