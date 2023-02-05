using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbreon.vaporeon.common
{
    public interface EditorInitiator<T>
    {
        public void init(T value);
    }
}
