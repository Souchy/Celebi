using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;

namespace Umbreon.data.resources
{
    public class SpellSkin : ISpellSkin
    {
        public IID entityUid { get; init; }
        public IID spellModelUid { get; set; }
        public IID icon { get; set; }
        public IID sourceAnimation { get; set; }
        public IID targetAnimation { get; set; }
        public IID behaviourScript { get; set; }


        public SpellSkin() { }
        public SpellSkin(IUIdGenerator uIdGenerator)
        {
            entityUid = uIdGenerator.next();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
