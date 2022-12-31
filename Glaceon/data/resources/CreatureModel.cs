using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using System.Collections.Generic;

namespace Celebi.data.resources
{
    public class CreatureModel
    {
        public IID id { get; set; }
        public List<IID> passiveStatusIds { get; init; } = new List<IID>();
        public IStats baseStats { get; set; }
        public List<IID> baseSpellIds { get; set; } = new List<IID>();
        public int[] skins;

    }
}
