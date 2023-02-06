using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.statuses
{
    public interface ICellStatus : IStatus
    {
        public IEntitySet<IID> cellIds { get; set; }

    }
}
