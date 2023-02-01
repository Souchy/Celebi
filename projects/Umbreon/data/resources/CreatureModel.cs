using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

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

        private CreatureModel() { }
        //private CreatureModel(IID id) => entityUid = id;
        public static ICreatureModel Create() => new CreatureModel() //Eevee.RegisterIID())
        {
            entityUid = Eevee.RegisterIID<ICreatureModel>(),
        };


        public void Dispose()
        {
            Eevee.DisposeIID<ICreatureModel>(entityUid);
        }

    }
}
