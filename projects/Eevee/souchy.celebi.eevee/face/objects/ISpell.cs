using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.objects
{
    public interface ISpell : IEntityModeled, IFightEntity
    {
        //public int chargesRemaining { get; set; }
        //public int cooldownRemaining { get; set; }
        //public int numberOfCastsThisTurn { get; }
        //public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; }
        public IID stats { get; set; }


        public IStats GetStats() => this.GetFight().stats.Get(stats);
        public ISpellModel GetModel() => Eevee.models.spellModels.Get(modelUid);
    }
}
