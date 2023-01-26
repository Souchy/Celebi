﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ICreatureModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public IID baseStats { get; init; }
        public HashSet<IID> baseSpells { get; init; }
        public HashSet<IID> baseStatusPassives { get; init; }

        public HashSet<IID> skins { get; init; }
    }
}
