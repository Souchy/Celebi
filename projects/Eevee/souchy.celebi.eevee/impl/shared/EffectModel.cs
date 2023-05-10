using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared
{
    public class EffectModel : IEffectModel
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
