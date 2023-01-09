using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using System.Collections.Generic;

namespace Umbreon.data.resources
{
    public class CreatureModel : ICreatureModel
    {
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public HashSet<IID> skins { get; init; }

        public IStats baseStats { get; init; }
        public HashSet<IID> baseSpells { get; init; }
        public HashSet<IID> baseStatusPassives { get; init; }


        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
