using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.statuses
{
    public interface ICellStatus : IStatusContainer
    {
        public IEntitySet<ObjectId> cellIds { get; set; }

    }
}
