using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace souchy.celebi.eevee.impl.shared
{
    public class EffectSkin : IEffectSkin
    {
        [BsonId]
        public ObjectId entityUid { get; set; }

        public IID vfxOnTarget { get; set; }
        public IID vfxOnSource { get; set; }
        public IID behaviourScript { get; set; }

        private EffectSkin() { }
        private EffectSkin(ObjectId id) => entityUid = id;
        public static IEffectSkin Create() => new EffectSkin(Eevee.RegisterIIDTemporary());

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }
    }
}
