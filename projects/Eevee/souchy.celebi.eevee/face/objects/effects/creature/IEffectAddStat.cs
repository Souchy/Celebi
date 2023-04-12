using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.face.objects.effects.creature
{
    public interface IEffectAddStat : IEffect
    {
        public CharacteristicId statId { get; set; }
        public IStat Stat { get; set; }

    }
}
