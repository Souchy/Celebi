using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.face.shared.effects.creature
{
    public interface IEffectAddStat : IEffect
    {

        public StatType StatType { get; set; }
        public IStat Stat { get; set; }

    }
}
