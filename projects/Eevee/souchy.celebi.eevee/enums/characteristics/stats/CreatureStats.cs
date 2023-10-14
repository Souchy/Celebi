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

        protected override IStats copyImplementation(bool anonymous = false)
        {
            if (anonymous) return new CreatureStats();
            else return CreatureStats.Create();
        }

        public void setStarterCurrentValues()
        {
            foreach (var res in Enum.GetValues<ResourceEnum>())
            {
                var current = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Current));
                var max = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Max));
                var initial = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.InitialMax));
                // some resources (shield) dont have a max etc
                if (max == null || initial == null) 
                    continue;
                // set current/max
                current.value = initial.value;
                max.value = initial.value;
                // 
                //if(current.statId != CharacteristicId.Default)  // we dont do default char anymore because that means the char doesnt exist
                    this.Set(current);
                //if (max.statId != CharacteristicId.Default)
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

                if (regen == null)
                    continue;

                if (regen.value == -1)
                {
                    if(max != null) 
                        current.value = max.value;
                    continue;
                }
                if(regen.value == 0)
                {
                    continue;
                }
                current.value += regen.value;

                if(max != null)
                    if (current.value > max.value)
                        current.value = max.value;

                continue;
            }
        }
    }
}
