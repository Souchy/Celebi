using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared
{
    public class EffectSkin : IEffectSkin
    {
        public IID entityUid { get; set; }

        public IID vfxOnTarget { get; set; }
        public IID vfxOnSource { get; set; }
        public IID behaviourScript { get; set; }

        private EffectSkin() { }
        private EffectSkin(IID id) => entityUid = id;
        public static IEffectSkin Create() => new EffectSkin(Eevee.RegisterIID<IEffectSkin>());

        public void Dispose()
        {
            Eevee.DisposeIID<IEffectSkin>(entityUid);
            throw new NotImplementedException();
        }
    }
}
