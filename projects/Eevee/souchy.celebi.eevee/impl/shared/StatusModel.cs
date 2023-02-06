using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared
{
    public class StatusModel : IStatusModel
    {
        public IID entityUid { get; set; }

        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }
        public List<IID> effectIds { get; set; }

        private StatusModel() { }
        private StatusModel(IID id) => entityUid = id;
        public static IStatusModel Create() => new StatusModel(Eevee.RegisterIID<IStatusModel>());

        public void Dispose()
        {
            Eevee.DisposeIID<IStatusModel>(entityUid);
            throw new NotImplementedException();
        }
    }
}
