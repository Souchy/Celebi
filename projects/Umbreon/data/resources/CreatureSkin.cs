using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using System.Collections.Generic;

namespace Umbreon.data.resources
{
    public class CreatureSkin : ICreatureSkin
    {
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public IID meshModel { get; set; }
        public IID icon { get; set; }
        public IID animations { get; set; }
        public Dictionary<IID, IID> spellSkins { get; set; }
        public Dictionary<IID, IID> effectSkins { get; set; } // FIXME This should go inside ISpellSkin


        public CreatureSkin() { }
        public CreatureSkin(IID id) => entityUid = id;
        public static ICreatureSkin Create() => new CreatureSkin(Eevee.RegisterIID())
        {
            nameId = Eevee.RegisterIID(),
            descriptionId = Eevee.RegisterIID(),
        };

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
