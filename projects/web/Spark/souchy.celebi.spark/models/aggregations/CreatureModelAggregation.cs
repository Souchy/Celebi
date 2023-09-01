﻿using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.spark.models.aggregations
{
    //public record CreatureModelTextAggregation
    //{
    //    [BsonId]
    //    public ObjectId entityUid { get; set; }
    //    public IID modelUid { get; set; }

    //    public IStringEntity nameId { get; set; }
    //    public IStringEntity descriptionId { get; set; }
    //}

    public record CreatureModelAggregation //: CreatureModelTextAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public CreatureStats stats { get; set; }
        public IEntitySet<SpellModelAggregation> spells { get; init; } = new EntitySet<SpellModelAggregation>();
        public IEntitySet<IStatusModel> statusPassives { get; init; } = new EntitySet<IStatusModel>();
        public IEntitySet<ICreatureSkin> skins { get; init; } = new EntitySet<ICreatureSkin>();
    }
}
