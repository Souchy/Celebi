using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IFightEntity : IEntity
    {
        [BsonSerializer(typeof(IIDSerializer))]
        public IID fightUid { get; set; }

        public IFight GetFight() => Eevee.fights.Get(fightUid); 

    }
}
