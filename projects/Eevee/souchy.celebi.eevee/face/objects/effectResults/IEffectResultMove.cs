using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.effectResults
{
    public interface IEffectResultMove : IEffectResult
    {
        public MoveType MoveType { get; set; }

        public IID newCell { get; set; }

    }
}
