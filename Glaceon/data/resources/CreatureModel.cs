using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using System.Collections.Generic;

namespace Celebi.data.resources
{
    public class CreatureModel : ICreatureModel
    {
        public IStats baseStats { get; init; }
        public List<IID> baseSpells { get; init; }
        public List<IID> baseStatusPassives { get; init; }
        public List<IID> skins { get; init; }
        public IID entityUid { get; init; }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
