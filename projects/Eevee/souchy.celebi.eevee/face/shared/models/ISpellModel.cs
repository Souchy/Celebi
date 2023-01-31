using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ISpellModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        //public List<ICost> costs { get; set; }
        public Dictionary<ResourceType, int> costs { get; set; }
        public ISpellProperties properties { get; set; }
        public HashSet<IID> effectIds { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
    }

    public interface ISpellProperties
    {
        /// <summary>
        /// Max number of charges you can keep in stock. <br></br>
        /// Gain one charge every cooldown. <br></br>
        /// Then you can cast all charges without cooldown.
        /// </summary>
        public IValue<int> maxCharges { get; set; }
        public IValue<int> maxCastsPerTurn { get; set; }
        public IValue<int> maxCastsPerTarget { get; set; }
        public IValue<int> cooldown { get; set; }
        public IValue<int> cooldownInitial { get; set; }
        public IValue<int> cooldownGlobal { get; set; }

        public IValue<int> minRange { get; set; }
        public IValue<int> maxRange { get; set; }
        public IValue<bool> castInDiagonal { get; set; }
        public IValue<bool> castInLine { get; set; }

        public IValue<bool> needLos { get; set; }
    }

    public interface ICost
    {
        public StatType resource { get; set; } // StatType, int, ResourceType
        public int value { get; set; }
    }


}