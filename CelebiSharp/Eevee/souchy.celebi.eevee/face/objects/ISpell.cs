using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee
{
    public interface ISpell : IEntityModeled
    {
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public List<ICost> costs { get; set; }
        public List<IID> effectIds { get; set; }

        public int maxCastsPerTurn { get; set; }
        public int maxCastsPerTarget { get; set; }
        public int cooldown { get; set; }
        public int cooldownInitial { get; set; }
        public int cooldownGlobal { get; set; }

    }

    public interface ICost
    {
        public StatType resource { get; set; } // StatType, int, ResourceType
        public int value { get; set; }
    }


}