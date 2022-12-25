using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee
{
    public interface ISpell : IEntityModeled
    {
        public ITargetFilter targetFilter { get; set; }
        public ICondition condition { get; set; }

        public List<ICost> costs { get; set; }
        public List<IEffect> effects { get; set; }

        public int maxCastsPerTurn { get; set; }
        public int maxCastsPerTarget { get; set; }
        public int cooldown { get; set; }
        public int cooldownInitial { get; set; }
        public int cooldownGlobal { get; set; }

    }

    public interface ICost
    {
        //public StatType stat { get; set; }
        public int statId { get; set; }
        public int val { get; set; }
    }


}