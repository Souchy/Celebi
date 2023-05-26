using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums
{
    /// <summary>
    /// This applies only if the number of instances in the container = the MaxStack stat
    /// </summary>
    public enum StatusMergeStrategy
    {
        Ignore,
        ReplaceLast,
    }
}
