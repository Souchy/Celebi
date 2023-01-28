using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.compiledeffects
{
    public interface ICompiledMove : ICompiledEffect
    {
        public MoveType MoveType { get; set; }

        public IID newCell { get; set; }

    }
}
