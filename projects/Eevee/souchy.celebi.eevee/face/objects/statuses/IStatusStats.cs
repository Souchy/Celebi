using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.face.objects.statuses
{
    public interface IStatusStats : IStatus
    {

        public IStats stats { get; set; }

    }
}
