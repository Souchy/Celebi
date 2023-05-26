using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.stats
{
    /// <summary>
    /// Maps a entity's ObjectId to a Stat (ex: number of spells cast per entity)
    /// </summary>
    public interface IEntityStatDictionary : IStat, IValue<Dictionary<ObjectId, IStat>>
    {
    }
}
