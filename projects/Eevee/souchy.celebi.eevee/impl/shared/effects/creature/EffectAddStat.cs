using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.effects.creature;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectAddStat : Effect, IEffectAddStat
    {

        public StatType statType { get; set; }
        public IStat stat { get; set; }

    }
}
