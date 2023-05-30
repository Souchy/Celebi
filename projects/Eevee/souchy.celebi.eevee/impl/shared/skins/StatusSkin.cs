using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.skins
{
    public class StatusSkin : IStatusSkin
    {
        [BsonId]
        public ObjectId entityUid { get; set; }

        /// <summary>
        /// Includes VFX and model animation, with the script
        /// </summary>
        public SceneIID sourceAnimation { get; set; } = new("");
        //public AnimationsData animations { get; set; }
        public SceneIID behaviourScript { get; set; } = new("");


        private StatusSkin() { }
        public static IStatusSkin Create() => new StatusSkin()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }
    }
}
