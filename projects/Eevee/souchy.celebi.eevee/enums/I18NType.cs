using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums
{
    /// <summary>
    /// TODO: delete this, just find every bundle folder in i18n/*lang*/files
    /// If you want a list of languages for things like [A select box to choose the game/site lang]
    /// Then maybe create a config file ewith the list of languages so that it's not hardcoded
    /// Then we can load the file into a (List<string> languages) inside a Settings kind of class 
    /// But we definitely dont need to directly address languages like I18n.fr, or .en. Just having a list is enough
    /// </summary>
    public enum I18NType
    {
        fr,
        en
    }
}
