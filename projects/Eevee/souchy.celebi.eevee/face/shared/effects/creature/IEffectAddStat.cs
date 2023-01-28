using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.face.shared.effects.creature
{
    public interface IEffectAddStat : IEffect
    {

        public StatType statType { get; set; }
        public IStat stat { get; set; }

    }
}
