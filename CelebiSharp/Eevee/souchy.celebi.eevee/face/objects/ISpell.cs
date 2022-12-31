using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.interfaces;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee
{
    public interface ISpell : IEntityModeled
    {
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public List<ICost> costs { get; set; }
        public ISpellProperties properties { get; set; }
        public List<IID> effectIds { get; set; }
    }

    public interface ISpellProperties
    {
        public IValueSingle<int> maxCastsPerTurn { get; set; }
        public IValueSingle<int> maxCastsPerTarget { get; set; }
        public IValueSingle<int> cooldown { get; set; }
        public IValueSingle<int> cooldownInitial { get; set; }
        public IValueSingle<int> cooldownGlobal { get; set; }
    }

    public interface ICost
    {
        public StatType resource { get; set; } // StatType, int, ResourceType
        public int value { get; set; }
    }


}