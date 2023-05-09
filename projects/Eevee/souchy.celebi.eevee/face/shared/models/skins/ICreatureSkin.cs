using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models.skins
{
    public interface ICreatureSkin : IEntity
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public string meshModel { get; set; }
        public string meshName { get; set; }
        public string icon { get; set; }
        public AnimationsData animations { get; set; }

        /// <summary>
        /// <spellModelid, spellSkinId>
        /// </summary>
        public Dictionary<ObjectId, ObjectId> spellSkins { get; set; }
        /// <summary>
        /// <effectId, effectSkinId>
        /// </summary>
        public Dictionary<ObjectId, ObjectId> effectSkins { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
    }

    public class AnimationsData
    {
        public string idle;
        public string run;
        public string walk;
        public string receiveHit;
        public string victory;
        public string defeat;
    }

}
