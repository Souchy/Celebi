using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.impl.shared
{
    public class SpellModel : ISpellModel
    {
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public Dictionary<ResourceType, int> costs { get; set; } = new();
        public ISpellProperties properties { get; set; } = new SpellProperties();
        public HashSet<IID> effectIds { get; set; } = new();

        public IZone RangeZoneMin { get; set; } = new Zone();
        public IZone RangeZoneMax { get; set; } = new Zone(); // Zone.Create();

        private SpellModel() { }
        private SpellModel(IID id) => entityUid = id;
        public static ISpellModel Create()
        {
            var model = new SpellModel(Eevee.RegisterIID<ISpellModel>());
            foreach (var resType in Enum.GetValues<ResourceType>())
                model.costs.Add(resType, 0);
            return model;
        }


        public void Dispose()
        {
            Eevee.DisposeIID<ISpellModel>(entityUid);
        }
    }


    public class SpellProperties : ISpellProperties
    {
        public IValue<int> maxCharges { get; set; } = new Value<int>();
        public IValue<int> maxCastsPerTurn { get; set; } = new Value<int>();
        public IValue<int> maxCastsPerTarget { get; set; } = new Value<int>();
        public IValue<int> cooldownInitial { get; set; } = new Value<int>();
        public IValue<int> cooldownGlobal { get; set; } = new Value<int>();
        public IValue<int> cooldown { get; set; } = new Value<int>();
        public IValue<int> minRange { get; set; } = new Value<int>();
        public IValue<int> maxRange { get; set; } = new Value<int>();
        public IValue<bool> castInDiagonal { get; set; } = new Value<bool>();
        public IValue<bool> castInLine { get; set; } = new Value<bool>();
        public IValue<bool> needLos { get; set; } = new Value<bool>(true);
    }

    //public class Cost : ICost
    //{
    //    public StatType resource { get; set; }
    //    public int value { get; set; }
    //}

}
