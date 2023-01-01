using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.skins
{
    public interface ICreatureSkin : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public IID meshModel { get; set; }
        public IID icon { get; set; }
        public IID animations { get; set; }

        public Dictionary<IID, IID> spellSkins { get; set; }
        public Dictionary<IID, IID> effectSkins { get; set; }
    }
}
