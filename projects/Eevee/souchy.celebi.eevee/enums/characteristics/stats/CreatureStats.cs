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
        public static new IStats Create() => new CreatureStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
        public void applyRegen()
        {
            foreach (var res in Enum.GetValues<ResourceEnum>())
            {
                var current = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Current));
                var regen = Get<IStatSimple>(Resource.getKey(res, ResourceProperty.Regen));
                current.value += regen.value;
            }
        }
    }
}
