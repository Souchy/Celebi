using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.shared
{
    public class SpellSkin : ISpellSkin
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        //public SpellIID spellModelUid { get; set; }
        public AssetIID icon { get; set; }
        public AssetIID sourceAnimation { get; set; }
        public AssetIID targetAnimation { get; set; }
        public AssetIID behaviourScript { get; set; }


        private SpellSkin() { }
        public static ISpellSkin Create() => new SpellSkin()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

    }
}
