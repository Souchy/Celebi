using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using System.Collections.Generic;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Umbreon.data.resources
{
    public class CreatureModel : ICreatureModel
    {
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public HashSet<IID> skins { get; init; } = new HashSet<IID>();

        public IID baseStats { get; init; }
        public HashSet<IID> baseSpells { get; init; } = new HashSet<IID>();
        public HashSet<IID> baseStatusPassives { get; init; } = new HashSet<IID>();

        public CreatureModel()
        {
        }
        public static CreatureModel Create()
        {
            return new CreatureModel()
            {
                entityUid = Eevee.RegisterIID(), 
                nameId = Eevee.RegisterIID(),
                descriptionId = Eevee.RegisterIID(), 
            };
        }

        public void Dispose()
        {
        }

    }
}
