using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.effects
{
    /// <summary>
    /// Effect that doesnt do anything. <br></br>
    /// It just holds other child effects and conditions and applies them in its aoe. <br></br>
    /// It can have a string to describe the condition or the children, example: <br></br>
    /// <code>
    /// <b>
    ///     Pour chaque Téléfrag généré :	<br></br>
    ///         Augmente les dommages finaux occasionnés de 200% 
    /// </b>
    /// </code>
    /// </summary>
    public interface IEffectBase : IEffect
    {
        public IID textId { get; set; }
    }
}
