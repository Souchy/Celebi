using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    public class SpellModelService : CollectionService<ISpellModel>
    {


        public SpellModelService(MongoModelsDbService service) : base(service.GetMongoCollection<ISpellModel>())
        {

        }


    }
}
