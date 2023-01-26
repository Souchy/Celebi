using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects
{

    /// <summary>
    /// This is the original intent for Context:
    ///     A place to copy the current context of the fight
    ///     and compute all effects without applying them to the real game
    ///     then you can use this for highlighting as well
    ///     and maybe replays (one context save for each action)
    /// </summary>
    public interface IActionContext
    {
        // wait so..
        // if all the data is stored in RedInstances or in IFight
        // then we just need to copy that 1 object and we'll be fine
        //


    }
}
