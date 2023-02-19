using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using System.Collections.Generic;

namespace souchy.celebi.eevee.impl.shared
{
    public class CreatureSkin : ICreatureSkin
    {
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public string meshModel { get; set; }
        public string meshName { get; set; }
        public string icon { get; set; }
        public AnimationsData animations { get; set; }


        /// <summary>
        /// <spellModelid, spellSkinId>
        /// </summary>
        public Dictionary<IID, IID> spellSkins { get; set; }
        /// <summary>
        /// <effectId, effectSkinId>
        /// </summary>
        public Dictionary<IID, IID> effectSkins { get; set; } // FIXME This should go inside ISpellSkin


        private CreatureSkin() { }
        public static ICreatureSkin Create() => new CreatureSkin()
        {
            entityUid = Eevee.RegisterIID<ICreatureSkin>(),
        };

        public void Dispose()
        {
            Eevee.DisposeIID<ICreatureSkin>(entityUid);
        }

    }
}
