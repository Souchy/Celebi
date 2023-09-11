using Microsoft.AspNetCore.DataProtection.KeyManagement;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.other
{
    public class CreatureStats : Stats
    {
        private CreatureStats() { }
        public static new CreatureStats Create() => new CreatureStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
        public void setStarterCurrentValues()
        {
            foreach (var res in Enum.GetValues<ResourceEnum>())
            {
                var current = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Current));
                var max = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Max));
                var initial = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.InitialMax));
                current.value = initial.value;
                max.value = initial.value;
                if(current.statId != CharacteristicId.Default) 
                    this.Set(current);
                if (max.statId != CharacteristicId.Default)
                    this.Set(max);
            }
        }
        public void applyRegen()
        {
            foreach (var res in Enum.GetValues<ResourceEnum>())
            {
                var current = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Current));
                var max = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Max));
                var regen = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Regen));
                if(regen.value == -1)
                {
                    current.value = max.value;
                    //if (current.statId != CharacteristicId.Default)
                    //    Set(current);
                    continue;
                }
                if(regen.value == 0)
                {
                    continue;
                }
                //if(regen.value == 1)
                //{
                current.value += regen.value;
                if (current.value > max.value)
                    current.value = max.value;
                //if (current.statId != CharacteristicId.Default)
                //    Set(current);
                continue;
                //}
                // else, 1 fois par x tours ou x = regen.value 
                // nan jpense pas, jpense c'est juste la valeur à regen directement.
                // par contre il faudrait un regen en % aussi
            }
        }
    }
}
