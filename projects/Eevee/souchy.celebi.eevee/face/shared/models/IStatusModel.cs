using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface IStatusModel : IEntity
    {
        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }

        public List<IID> effectIds { get; set; }

    }
}
