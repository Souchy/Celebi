using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.statuses
{
    public interface ICellStatus : IStatus
    {
        public List<IID> cellIds { get; set; }

    }
}
