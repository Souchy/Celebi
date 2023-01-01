using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.interfaces;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.models
{
    /// <summary>
    /// Spell Prototype, to be copied 
    /// </summary>
    public interface ISpellModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public List<ICost> costs { get; set; }
        public ISpellProperties properties { get; set; }

        public HashSet<IID> effectIds { get; set; }
    }

    public interface ISpellProperties
    {
        public IValue<int> maxCastsPerTurn { get; set; }
        public IValue<int> maxCastsPerTarget { get; set; }
        public IValue<int> cooldown { get; set; }
        public IValue<int> cooldownInitial { get; set; }
        public IValue<int> cooldownGlobal { get; set; }
    }

    public interface ICost
    {
        public StatType resource { get; set; } // StatType, int, ResourceType
        public int value { get; set; }
    }


}