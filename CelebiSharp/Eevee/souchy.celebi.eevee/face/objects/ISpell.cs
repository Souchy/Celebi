using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects
{
    public interface ISpell : IEntityModeled
    {
        public int chargesRemaining { get; set; }
        public int cooldownRemaining { get; set; }
        public int numberOfCastsThisTurn { get; set; }
        public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; }
    }
}
