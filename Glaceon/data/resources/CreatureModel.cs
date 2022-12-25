using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using System.Collections.Generic;

namespace Celebi.data.resources
{
    public class CreatureModel
    {
        public uint id { get; set; }
        public List<IStatus> passives { get; init; } = new List<IStatus>();
        public IStats baseStats { get; set; }
        public List<int> baseSpells { get; set; } = new List<int>();
        public int[] skins;

    }
}
