using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStatDetailed : IStat, IValue<int>
    {

        public int baseFlat { get; init; }
        public int increasedPercent { get; init; }
        public int increasedFlat { get; init; }
        public int morePercent { get; init; }

    }
}
