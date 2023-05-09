﻿using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ISpellModel : IEntityModel, IEffectsContainer
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public Dictionary<CharacteristicId, int> costs { get; set; }
        //public SpellProperties properties { get; set; }
        public ObjectId stats { get; set; }

        public IZone RangeZoneMin { get; set; }
        public IZone RangeZoneMax { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
        public IStats GetStats() => Eevee.models.stats.Get(stats);
    }

    public struct SpellProperties
    {
        public SpellProperties()
        {
        }
        /// <summary>
        /// Max number of charges you can keep in stock. <br></br>
        /// Gain one charge every cooldown. <br></br>
        /// Then you can cast all charges without cooldown.
        /// </summary>
        public IValue<int> maxCharges { get; set; } = new Value<int>();
        public IValue<int> maxCastsPerTurn { get; set; } = new Value<int>();
        public IValue<int> maxCastsPerTarget { get; set; } = new Value<int>();
        // cooldowns
        public IValue<int> cooldownInitial { get; set; } = new Value<int>();
        public IValue<int> cooldownGlobal { get; set; } = new Value<int>();
        public IValue<int> cooldown { get; set; } = new Value<int>();
        // range
        public IValue<int> minRange { get; set; } = new Value<int>();
        public IValue<int> maxRange { get; set; } = new Value<int>();
        public IValue<bool> castInDiagonal { get; set; } = new Value<bool>();
        public IValue<bool> castInLine { get; set; } = new Value<bool>();
        public IValue<bool> needLos { get; set; } = new Value<bool>(true);
    }


}