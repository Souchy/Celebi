using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.statuses
{
    public interface ICellStatus : IStatus
    {
        public List<ICell> cells { get; set; }

    }
}
