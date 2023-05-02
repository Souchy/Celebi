using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.shared
{
    public class CreatureModel : ICreatureModel
    {
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public IEntitySet<IID> skins { get; init; } = new EntitySet<IID>();

        public IID baseStats { get; set; }
        public IID growthStats { get; set; }
        public IEntitySet<IID> baseSpells { get; init; } = new EntitySet<IID>();
        public IEntitySet<IID> baseStatusPassives { get; init; } = new EntitySet<IID>();

        private CreatureModel() { }
        public static ICreatureModel CreatePermanent() => new CreatureModel() 
        {
            entityUid = Eevee.RegisterIID<ICreatureModel>(),
        };


        public void Dispose()
        {
            Eevee.DisposeIID<ICreatureModel>(entityUid);
        }

    }
}
