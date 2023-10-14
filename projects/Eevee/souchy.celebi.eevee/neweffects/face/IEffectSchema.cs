using souchy.celebi.eevee.neweffects.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.face
{
    public interface IEffectSchema
    {
        public EffT effType
        {
            get
            {
                return Enum.Parse<EffT>(this.GetType().Name);
            }
        }
        public IEffectSchema copy();
}
}
