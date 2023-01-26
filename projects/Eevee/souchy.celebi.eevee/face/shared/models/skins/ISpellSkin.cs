using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models.skins
{
    /// <summary>
    /// ModelID = SpellModelID
    /// </summary>
    public interface ISpellSkin : IEntity
    {
        public IID spellModelUid { get; set; }
        public IID icon { get; set; }
        public IID sourceAnimation { get; set; }
        public IID targetAnimation { get; set; }
        public IID behaviourScript { get; set; }
    }
}
