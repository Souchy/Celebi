﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace Umbreon.data.resources
{
    public class SpellSkin : ISpellSkin
    {
        public IID entityUid { get; set; }
        public IID spellModelUid { get; set; }
        public IID icon { get; set; }
        public IID sourceAnimation { get; set; }
        public IID targetAnimation { get; set; }
        public IID behaviourScript { get; set; }


        public SpellSkin() { }
        public SpellSkin(IID id) => entityUid = id;
        public static ISpellSkin Create() => new SpellSkin(Eevee.RegisterIID());

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
