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
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public HashSet<IID> skins { get; init; } = new HashSet<IID>();

        public IID baseStats { get; init; }
        public HashSet<IID> baseSpells { get; init; } = new HashSet<IID>();
        public HashSet<IID> baseStatusPassives { get; init; } = new HashSet<IID>();

        public CreatureModel() { }
        //private CreatureModel(IID id) => entityUid = id;
        public static ICreatureModel Create() => new CreatureModel() //Eevee.RegisterIID())
        {
            entityUid = Eevee.RegisterIID<ICreatureModel>(),
            nameId = Eevee.RegisterIID<string>(),
            descriptionId = Eevee.RegisterIID<string>(),
        };

        public void Dispose()
        {
        }

    }
}
