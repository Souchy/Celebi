using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects
{
    public interface ISpell : IEntityModeled, IFightEntity
    {
        //public int chargesRemaining { get; set; }
        //public int cooldownRemaining { get; set; }
        //public int numberOfCastsThisTurn { get; }
        //public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; }
        public ObjectId statsId { get; set; }


        public SpellStats GetStats() => (SpellStats) this.GetFight().stats.Get(statsId);
        public ISpellModel GetModel() => Eevee.models.spellModels.Values.First(s => s.modelUid == this.modelUid); //.Get(entityUid);
    }
}
