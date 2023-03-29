using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.other
{
    public class CreatureStats
    {
        private Dictionary<IID, IStat> stats = new();
        // -1 = full every turn, 0 = no regen, 1 = 1/turn, 0.25 = 1 per 4 turns // string to parse for different regens
        //private readonly Dictionary<string, IValue<int>> resourceRegen = new();
        // gain x every turn
        private readonly Dictionary<IID, IValue<int>> growth = new();
        int turnsBetweenGrowths;

        public T get<T>(CharacteristicType<T> ct) where T : IStat
        {
            return (T) stats[ct.id];
        }
        public T get<T>(IID id) where T : IStat
        {
            return (T) stats[id];
        }
        public int getGrowth<T>(CharacteristicType<T> ct) where T : IStat
        {
            return growth[ct.id].value;
        }
        public void applyRegen()
        {
            foreach (var res in Enum.GetValues<ResourceEnum>())
            {
                var current = get(Resource.getKey(res, ResourceProperty.Current));
                var regen = get(Resource.getKey(res, ResourceProperty.Regen));
                current.value += regen.value;
            }
        }
        public void applyGrowth()
        {
            foreach (var key in growth.Keys)
            {
                var grow = growth[key].value;
                var charac = get<IStatSimple>(key);
                charac.value += grow;
                // if StatResource : current += grow & max += grow
            }
        }
    }
}
