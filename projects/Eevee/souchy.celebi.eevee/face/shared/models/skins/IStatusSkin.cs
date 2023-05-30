using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.shared.models.skins
{
    public interface IStatusSkin : IEntity
    {
        /// <summary>
        /// Scene contains the VFX and creature animation scripts stuffs
        /// </summary>
        public SceneIID sourceAnimation { get; set; }   //public AnimationsData animations { get; set; }
        public SceneIID behaviourScript { get; set; }
    }
}
