using souchy.celebi.eevee.face.stats;

namespace souchy.celebi.eevee.face.statuses
{
    public interface IStatusStats : IStatus
    {

        public IStats stats { get; set; }

    }
}
