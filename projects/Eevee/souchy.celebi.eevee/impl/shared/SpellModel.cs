using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.impl.shared
{
    public class SpellModel : ISpellModel
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public ObjectId statsId { get; set; }
        //public SpellProperties properties { get; set; } = new SpellProperties();

        public Dictionary<CharacteristicId, int> costs { get; set; } = new();
        public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>(); 

        public IZone RangeZoneMin { get; set; } = new Zone();
        public IZone RangeZoneMax { get; set; } = new Zone();

        private SpellModel() { }
        private SpellModel(ObjectId id) => entityUid = id;
        public static ISpellModel CreatePermanent()
        {
            var model = new SpellModel(Eevee.RegisterIIDTemporary());
            //foreach (var resType in Enum.GetValues<ResourceEnum>())
            //{
            //    var res = Resource.getKey(resType, ResourceProperty.Current);
            //    model.costs.Add(res.ID, 0);
            //}
            return model;
        }


        public IEnumerable<IEffect> GetEffects() => EffectIds.Values.Select(i => Eevee.models.effects.Get(i));

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

    }

    /*
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
    */

    //public class Cost : ICost
    //{
    //    public StatType resource { get; set; }
    //    public int value { get; set; }
    //}

}
