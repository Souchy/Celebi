using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.shared
{
    public class SpellSkin : ISpellSkin
    {
        public ObjectId entityUid { get; set; }
        public IID spellModelUid { get; set; }
        public IID icon { get; set; }
        public IID sourceAnimation { get; set; }
        public IID targetAnimation { get; set; }
        public IID behaviourScript { get; set; }


        private SpellSkin() { }
        private SpellSkin(ObjectId id) => entityUid = id;
        public static ISpellSkin Create() => new SpellSkin(Eevee.RegisterIIDTemporary());

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }

    }
}
